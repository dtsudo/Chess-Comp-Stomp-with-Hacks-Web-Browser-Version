
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class BridgeMusic : IMusic<GameMusic>
	{
		private GameMusic? currentGameMusic;
		private int currentVolume;
		private bool isMobileSafari;
		
		public BridgeMusic(bool stopWaitingEvenIfMusicHasNotLoaded)
		{
			this.currentGameMusic = null;
			this.currentVolume = 0;
			this.isMobileSafari = BridgeUtil.IsMobileSafari();
			
			if (this.isMobileSafari)
			{
				Script.Eval(@"
					window.BridgeMusicJavascript = ((function () {
						'use strict';
						
						let musicDictionary = {};
						let bufferSource = null;
						let currentlyPlayingMusic = null;
						let gainNode = null;
										
						let numberOfAudioObjectsLoaded = 0;
						let numberOfAudioObjects = null;
										
						let audioContext = new AudioContext();

						let loadMusic = function (oggMusicNames, flacMusicNames) {
							let oggMusicNamesArray = oggMusicNames.split(',');
							let flacMusicNamesArray = flacMusicNames.split(',');

							numberOfAudioObjects = oggMusicNamesArray.length;
												
							for (let i = 0; i < oggMusicNamesArray.length; i++) {
								let musicName = oggMusicNamesArray[i];
								let flacMusicName = flacMusicNamesArray[i];
							
								if (musicDictionary[musicName])
									continue;

								numberOfAudioObjectsLoaded++;
								musicDictionary[musicName] = { audioBuffer: null };
										
								let musicPath = 'Data/Music/' + flacMusicName + '?doNotCache=' + Date.now().toString();

								fetch(musicPath)
									.then(response => response.blob())
									.then(blob => blob.arrayBuffer())
									.then(arrayBuffer => audioContext.decodeAudioData(arrayBuffer))
									.then(audioBuffer => { musicDictionary[musicName].audioBuffer = audioBuffer; });
							}
						
							return true;
						};

						let getNumElementsLoaded = function () {
							return numberOfAudioObjectsLoaded;
						};
					
						let getNumTotalElementsToLoad = function () {
							return numberOfAudioObjects;
						};

						var musicCounter = 0;
					
						var playMusic = function (musicName, volume) {
							musicCounter++;
							var currentMusicCounter = musicCounter;
							var music = musicDictionary[musicName];
						
							if (volume > 1.0)
								volume = 1.0;
							if (volume < 0.0)
								volume = 0.0;
									
							if (music.audioBuffer === null) {
								setTimeout(function () {
									if (currentMusicCounter === musicCounter)
										playMusic(musicName, volume);
								}, 250);
								return;
							}

							if (audioContext.state === 'suspended') {
								currentlyPlayingMusic = null;
								if (bufferSource !== null)
									bufferSource.stop();
								bufferSource = null;
								gainNode = null;
							}

							audioContext.resume().then(() => {
								if (currentMusicCounter !== musicCounter)
									return;

								if (currentlyPlayingMusic === musicName) {
									gainNode.gain.setValueAtTime(volume, 0);
									return;
								}

								currentlyPlayingMusic = musicName;
								if (bufferSource !== null)
									bufferSource.stop();
								bufferSource = audioContext.createBufferSource();
								bufferSource.buffer = music.audioBuffer;
								bufferSource.loop = true;
								gainNode = new GainNode(audioContext, { gain: volume });
								bufferSource.connect(gainNode);
								gainNode.connect(audioContext.destination);
								bufferSource.start();

								if (audioContext.state === 'suspended') {
									currentlyPlayingMusic = null;
									if (bufferSource !== null)
										bufferSource.stop();
									bufferSource = null;
									gainNode = null;
								}
							});
						};
					
						var stopMusic = function () {
							musicCounter++;
							currentlyPlayingMusic = null;
							if (bufferSource !== null)
								bufferSource.stop();
							bufferSource = null;
							gainNode = null;
						};
					
						return {
							loadMusic: loadMusic,
							getNumElementsLoaded: getNumElementsLoaded,
							getNumTotalElementsToLoad: getNumTotalElementsToLoad,
							playMusic: playMusic,
							stopMusic: stopMusic
						};
					})());
				");
			}
			else
			{
				Script.Eval(@"
					window.BridgeMusicJavascript = ((function () {
						'use strict';
						
						let musicDictionary = {};
					
						let stopWaitingEvenIfMusicHasNotLoaded = " + (stopWaitingEvenIfMusicHasNotLoaded ? "true" : "false") + @";
					
						let numberOfAudioObjectsLoaded = 0;
						let numberOfAudioObjects = null;					

						let stopWaiting = false;
					
						if (stopWaitingEvenIfMusicHasNotLoaded)
							setTimeout(function () { stopWaiting = true; }, 2000);
					
						let loadMusic = function (oggMusicNames, flacMusicNames) {
							let oggMusicNamesArray = oggMusicNames.split(',');
							let flacMusicNamesArray = flacMusicNames.split(',');
						
							numberOfAudioObjects = oggMusicNamesArray.length;
						
							for (let i = 0; i < oggMusicNamesArray.length; i++) {
								let musicName = oggMusicNamesArray[i];
								let flacMusicName = flacMusicNamesArray[i];
							
								if (musicDictionary[musicName])
									continue;
										
								let musicPath = 'Data/Music/' + musicName + '?doNotCache=' + Date.now().toString();
							
								let hasAudioLoadingSucceeded = false;
								let audio = new Audio();
								audio.addEventListener('canplaythrough', function () {
									if (!hasAudioLoadingSucceeded) {
										numberOfAudioObjectsLoaded++;
									}
									hasAudioLoadingSucceeded = true;
								});
								audio.src = musicPath;
								audio.load();
								audio.loop = true;

								let checkForError;
								checkForError = function () {
									if (hasAudioLoadingSucceeded)
										return;
									if (audio.error !== null) {
										audio.src = 'Data/Music/' + flacMusicName + '?doNotCache=' + Date.now().toString();
										audio.load();
										audio.loop = true;
										return;
									}

									setTimeout(checkForError, 50 /* arbitrary */);
								};
								setTimeout(checkForError, 0);
							
								musicDictionary[musicName] = audio;
							}
						
							if (stopWaiting)
								return true;
						
							return numberOfAudioObjects === numberOfAudioObjectsLoaded;
						};

						let getNumElementsLoaded = function () {
							return numberOfAudioObjectsLoaded;
						};

						let getNumTotalElementsToLoad = function () {
							return numberOfAudioObjects;
						};
					
						var musicCounter = 0;
					
						var playMusic = function (musicName, volume) {
							musicCounter++;
							var currentMusicCounter = musicCounter;
							var music = musicDictionary[musicName];
						
							if (volume > 1.0)
								volume = 1.0;
							if (volume < 0.0)
								volume = 0.0;
										
							for (var m in musicDictionary) {
								var audio = musicDictionary[m];
							
								if (audio === music) {
									audio.volume = volume;
									var audioPromise = audio.play();
									if (audioPromise) {
										audioPromise.then(
											function () {},
											function () {
												setTimeout(function () {
													if (currentMusicCounter === musicCounter)
														playMusic(musicName, volume);
												}, 50);
											});
									}
								} else {
									audio.pause();
									audio.currentTime = 0;
								}
							}
						};
					
						var stopMusic = function () {
							musicCounter++;
							for (var musicName in musicDictionary) {
								var audio = musicDictionary[musicName];
								audio.pause();
								audio.currentTime = 0;
							}
						};
					
						return {
							loadMusic: loadMusic,
							getNumElementsLoaded: getNumElementsLoaded,
							getNumTotalElementsToLoad: getNumTotalElementsToLoad,
							playMusic: playMusic,
							stopMusic: stopMusic
						};
					})());
				");
			}
		}
		
		public bool LoadMusic()
		{
			string oggMusicNames = "";
			string flacMusicNames = "";
			bool isFirst = true;
			foreach (GameMusic gameMusic in Enum.GetValues(typeof(GameMusic)))
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else
				{
					oggMusicNames = oggMusicNames + ",";
					flacMusicNames = flacMusicNames + ",";
				}
				oggMusicNames = oggMusicNames + gameMusic.GetMusicFilename().OggFilename;
				flacMusicNames = flacMusicNames + gameMusic.GetMusicFilename().FlacFilename;
			}
			
			if (oggMusicNames == "")
				return true;
			
			bool result = Script.Eval<bool>("window.BridgeMusicJavascript.loadMusic('" + oggMusicNames + "', '" + flacMusicNames + "')");
			
			return result;
		}

		public int GetNumElementsLoaded()
		{
			return Script.Call<int>("window.BridgeMusicJavascript.getNumElementsLoaded");
		}

		public int? GetNumTotalElementsToLoad()
		{
			return Script.Call<int?>("window.BridgeMusicJavascript.getNumTotalElementsToLoad");
		}

		public void PlayMusic(GameMusic music, int volume)
		{
			if (this.currentGameMusic.HasValue
					&& this.currentGameMusic.Value == music
					&& this.currentVolume == volume
					&& !this.isMobileSafari)
				return;
			
			this.currentGameMusic = music;
			this.currentVolume = volume;
			
			double finalVolume = (music.GetMusicVolume() / 100.0) * (volume / 100.0);
			if (finalVolume > 1.0)
				finalVolume = 1.0;
			if (finalVolume < 0.0)
				finalVolume = 0.0;
			
			Script.Call("window.BridgeMusicJavascript.playMusic", music.GetMusicFilename().OggFilename, finalVolume);
		}
		
		public void StopMusic()
		{
			if (this.currentGameMusic == null && !this.isMobileSafari)
				return;
			
			this.currentGameMusic = null;
			
			Script.Call("window.BridgeMusicJavascript.stopMusic");
		}
		
		public void DisposeMusic()
		{
		}
	}
}
