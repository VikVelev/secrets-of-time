﻿/*
 * unityGB
 * The MIT License (MIT)
 *
 * Copyright (C) 2014 Jonathan Odul (jona@takohi.com)
 *
 * This file is part of unityGB.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#pragma warning disable 0414
using System.Collections;

/// <summary>
/// Voluntary wave generator.
/// Based on JavaBoy source.
/// </summary>

namespace UnityGB
{
	public class VoluntaryWaveGenerator
	{
		public const int CHAN_LEFT = 1;
		public const int CHAN_RIGHT = 2;
		public const int CHAN_MONO = 4;

		private int totalLength;
		private int cyclePos;
		private int cycleLength;
		private int amplitude;
		private int channel;
		private int sampleRate;
		private int volumeShift;
		private int[] waveform = new int[32];
	
		public VoluntaryWaveGenerator(int waveLength, int ampl, int duty, int chan, int rate)
		{
			cycleLength = waveLength;
			amplitude = ampl;
			cyclePos = 0;
			channel = chan;
			sampleRate = rate;
		}
	
		public VoluntaryWaveGenerator(int rate)
		{
			cyclePos = 0;
			channel = CHAN_LEFT | CHAN_RIGHT;
			cycleLength = 2;
			totalLength = 0;
			sampleRate = rate;
			amplitude = 32;
		}
	
		public void SetSampleRate(int sr)
		{
			sampleRate = sr;
		}
	
		public void SetFrequency(int gbFrequency)
		{
			float frequency = 65536f / (float)(2048 - gbFrequency);
			cycleLength = (int)((float)(256f * sampleRate) / frequency);
			if (cycleLength == 0)
				cycleLength = 1;
		}
	
		public void SetChannel(int chan)
		{
			channel = chan;
		}
	
		public void SetLength(int gbLength)
		{
			if (gbLength == -1)
			{
				totalLength = -1;
			} else
			{
				totalLength = (256 - gbLength) / 4;
			}
		}
	
		public void SetSamplePair(int address, int value)
		{
			waveform [address * 2] = (value & 0xF0) >> 4;
			waveform [address * 2 + 1] = (value & 0x0F);
		}
	
		public void SetVolume(int volume)
		{
			switch (volume)
			{
				case 0:
					volumeShift = 5;
					break;
				case 1:
					volumeShift = 0;
					break;
				case 2:
					volumeShift = 1;
					break;
				case 3:
					volumeShift = 2;
					break;
			}
		}
	
		public void Play(byte[] b, int numSamples, int numChannels)
		{
			int val;
		
			if (totalLength > 0)
			{
				totalLength--;
			
				for (int r = 0; r < numSamples; r++)
				{
					int samplePos = (31 * cyclePos) / cycleLength;
					val = waveform [samplePos % 32] >> volumeShift << 1;
				
					if ((channel & CHAN_LEFT) != 0)
						b [r * numChannels] += (byte)val;
					if ((channel & CHAN_RIGHT) != 0)
						b [r * numChannels + 1] += (byte)val;
					//if ((channel & CHAN_MONO) != 0)
					//b [r * numChannels] = b [r * numChannels + 1] += (byte)val;

					cyclePos = (cyclePos + 256) % cycleLength;
				}
			}
		}
	}
}
