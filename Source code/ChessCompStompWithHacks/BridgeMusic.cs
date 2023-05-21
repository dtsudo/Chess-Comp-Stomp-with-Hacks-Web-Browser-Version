
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
		
		public BridgeMusic()
		{
			this.currentGameMusic = null;
			this.currentVolume = 0;
			
			Script.Eval(@"
				window.BridgeMusicJavascript = ((function () {
					'use strict';
						
					var musicDictionary = {};
					
					var numberOfAudioObjectsLoaded = 0;
					
					var loadMusic = function (musicNames) {
						var musicNamesArray = musicNames.split(',');
						
						var numberOfAudioObjects = musicNamesArray.length;
						
						for (var i = 0; i < musicNamesArray.length; i++) {
							var musicName = musicNamesArray[i];
							
							if (musicDictionary[musicName])
								continue;
										
							var musicPath = 'Data/Music/' + musicName + '?doNotCache=' + Date.now().toString();
							
							var audio = new Audio(musicPath);
							audio.addEventListener('canplaythrough', function () {
								numberOfAudioObjectsLoaded++;
							});
							audio.loop = true;
							
							musicDictionary[musicName] = audio;
						}
						
						return numberOfAudioObjects === numberOfAudioObjectsLoaded;
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
						playMusic: playMusic,
						stopMusic: stopMusic
					};
				})());
			");
		}
		
		public bool LoadMusic()
		{
			string musicNames = "";
			bool isFirst = true;
			foreach (GameMusic gameMusic in Enum.GetValues(typeof(GameMusic)))
			{
				if (isFirst)
					isFirst = false;
				else
					musicNames = musicNames + ",";
				musicNames = musicNames + gameMusic.GetMusicFilename().DefaultFilename;
			}
			
			if (musicNames == "")
				return true;
			
			bool result = Script.Eval<bool>("window.BridgeMusicJavascript.loadMusic('" + musicNames + "')");
			
			return result;
		}
		
		public void PlayMusic(GameMusic music, int volume)
		{
			if (this.currentGameMusic.HasValue
					&& this.currentGameMusic.Value == music
					&& this.currentVolume == volume)
				return;
			
			this.currentGameMusic = music;
			this.currentVolume = volume;
			
			double finalVolume = (music.GetMusicVolume() / 100.0) * (volume / 100.0);
			if (finalVolume > 1.0)
				finalVolume = 1.0;
			if (finalVolume < 0.0)
				finalVolume = 0.0;
			
			Script.Call("window.BridgeMusicJavascript.playMusic", music.GetMusicFilename().DefaultFilename, finalVolume);
		}
		
		public void StopMusic()
		{
			if (this.currentGameMusic == null)
				return;
			
			this.currentGameMusic = null;
			
			Script.Call("window.BridgeMusicJavascript.stopMusic");
		}
		
		public void DisposeMusic()
		{
		}
	}
}
