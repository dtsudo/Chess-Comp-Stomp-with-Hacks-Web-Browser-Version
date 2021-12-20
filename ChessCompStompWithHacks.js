/**
 * @compiler Bridge.NET 17.6.0
 */
Bridge.assembly("ChessCompStompWithHacks", function ($asm, globals) {
    "use strict";

    Bridge.define("DTLibrary.IDisplayCleanup", {
        $kind: "interface"
    });

    Bridge.definei("DTLibrary.IDisplayOutput$2", function (ImageEnum, FontEnum) { return {
        $kind: "interface"
    }; });

    Bridge.definei("DTLibrary.IDisplayProcessing$1", function (ImageEnum) { return {
        $kind: "interface"
    }; });

    Bridge.define("ChessCompStompWithHacks.BridgeDisplayFont", {
        ctors: {
            ctor: function () {
                this.$initialize();
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeDisplayFontJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar fontDictionary = {};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar context = null;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar fontFamilyCount = 0;\r\n\t\t\t\t\tvar numberOfFontObjectsLoaded = 0;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar loadFonts = function (fontNames) {\r\n\t\t\t\t\t\tvar fontNamesArray = fontNames.split(',');\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar numberOfFontObjects = fontNamesArray.length;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < fontNamesArray.length; i++) {\r\n\t\t\t\t\t\t\tvar fontName = fontNamesArray[i];\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (fontDictionary[fontName])\r\n\t\t\t\t\t\t\t\tcontinue;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tvar fontFamilyName = 'chessCompStompFontFamily' + fontFamilyCount;\r\n\t\t\t\t\t\t\tfontFamilyCount++;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tvar font = new FontFace(fontFamilyName, 'url(Data/Font/' + fontName + ')');\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tfontDictionary[fontName] = {\r\n\t\t\t\t\t\t\t\tfont: font,\r\n\t\t\t\t\t\t\t\tfontFamilyName: fontFamilyName\r\n\t\t\t\t\t\t\t};\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tfont.load().then((function (f) {\r\n\t\t\t\t\t\t\t\treturn function () {\r\n\t\t\t\t\t\t\t\t\tdocument.fonts.add(f);\r\n\t\t\t\t\t\t\t\t\tnumberOfFontObjectsLoaded++;\r\n\t\t\t\t\t\t\t\t};\r\n\t\t\t\t\t\t\t})(font));\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\treturn numberOfFontObjects === numberOfFontObjectsLoaded;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\t\r\n\t\t\t\t\tvar drawText = function (x, y, str, fontName, javascriptFontSize, javascriptLineHeight, red, green, blue, alpha) {\r\n\t\t\t\t\t\tif (context === null) {\r\n\t\t\t\t\t\t\tvar canvas = document.getElementById('chessCompStompWithHacksCanvas');\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (canvas === null)\r\n\t\t\t\t\t\t\t\treturn;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tcontext = canvas.getContext('2d');\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tjavascriptLineHeight = parseFloat(javascriptLineHeight);\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.textBaseline = 'top';\r\n\t\t\t\t\t\tcontext.fillStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';\r\n\t\t\t\t\t\tcontext.strokeStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';\r\n\t\t\t\t\t\tcontext.font = javascriptFontSize + 'px \"' + fontDictionary[fontName].fontFamilyName + '\"';\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar strArray = str.split('\\n');\r\n\t\t\t\t\t\tvar lineY = y;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < strArray.length; i++) {\r\n\t\t\t\t\t\t\tcontext.fillText(strArray[i], x, Math.round(lineY));\r\n\t\t\t\t\t\t\tlineY += javascriptLineHeight;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tloadFonts: loadFonts,\r\n\t\t\t\t\t\tdrawText: drawText\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            LoadFonts: function () {
                var $t, $t1;
                // Two fonts might have the same WoffFontFilename
                var woffFontFilenames = new (System.Collections.Generic.HashSet$1(System.String)).ctor();

                $t = Bridge.getEnumerator(System.Enum.getValues(ChessCompStompWithHacksLibrary.ChessFont));
                try {
                    while ($t.moveNext()) {
                        var font = Bridge.cast($t.Current, ChessCompStompWithHacksLibrary.ChessFont);
                        woffFontFilenames.add(ChessCompStompWithHacksLibrary.ChessFontUtil.GetFontInfo(font).WoffFontFilename);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                var woffFontFilenamesAsString = "";
                var isFirst = true;
                $t1 = Bridge.getEnumerator(woffFontFilenames);
                try {
                    while ($t1.moveNext()) {
                        var woffFontFilename = $t1.Current;
                        if (isFirst) {
                            isFirst = false;
                        } else {
                            woffFontFilenamesAsString = (woffFontFilenamesAsString || "") + ",";
                        }
                        woffFontFilenamesAsString = (woffFontFilenamesAsString || "") + (woffFontFilename || "");
                    }
                } finally {
                    if (Bridge.is($t1, System.IDisposable)) {
                        $t1.System$IDisposable$Dispose();
                    }
                }

                if (Bridge.referenceEquals(woffFontFilenamesAsString, "")) {
                    return true;
                }

                return eval("window.ChessCompStompWithHacksBridgeDisplayFontJavascript.loadFonts('" + (woffFontFilenamesAsString || "") + "')");
            },
            DrawText: function (x, y, text, font, color) {
                y = (((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT - y) | 0) - 1) | 0;

                var red = color.R;
                var green = color.G;
                var blue = color.B;
                var alpha = color.Alpha;

                var fontInfo = ChessCompStompWithHacksLibrary.ChessFontUtil.GetFontInfo(font);

                window.ChessCompStompWithHacksBridgeDisplayFontJavascript.drawText(x, y, text, fontInfo.WoffFontFilename, fontInfo.JavascriptFontSize, fontInfo.JavascriptLineHeight, red, green, blue, alpha);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeDisplayImages", {
        fields: {
            widthDictionary: null,
            heightDictionary: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                this.widthDictionary = new (System.Collections.Generic.Dictionary$2(ChessCompStompWithHacksLibrary.ChessImage,System.Int32))();
                this.heightDictionary = new (System.Collections.Generic.Dictionary$2(ChessCompStompWithHacksLibrary.ChessImage,System.Int32))();
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeDisplayImagesJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar imgDict = {};\r\n\t\t\t\t\tvar widthDict = {};\r\n\t\t\t\t\tvar heightDict = {};\r\n\t\t\t\t\tvar canvas = null;\r\n\t\t\t\t\tvar context = null;\r\n\t\t\t\t\tvar radianConversion = 1.0 / 128.0 * (2.0 * Math.PI / 360.0);\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar numberOfImagesLoaded = 0;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar loadImages = function (imageNames) {\r\n\t\t\t\t\t\tvar imageNamesArray = imageNames.split(',');\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar count = 0;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < imageNamesArray.length; i++) {\r\n\t\t\t\t\t\t\tvar imageName = imageNamesArray[i];\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (imgDict[imageName])\r\n\t\t\t\t\t\t\t\tcontinue;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tvar imgPath = 'Data/Images/' + imageName;\r\n\t\t\t\t\t\t\tvar img = new Image();\r\n\t\t\t\t\t\t\timg.addEventListener('load', (function (a, b) {\r\n\t\t\t\t\t\t\t\treturn function () {\r\n\t\t\t\t\t\t\t\t\tnumberOfImagesLoaded++;\r\n\t\t\t\t\t\t\t\t\twidthDict[a] = b.naturalWidth;\r\n\t\t\t\t\t\t\t\t\theightDict[a] = b.naturalHeight;\r\n\t\t\t\t\t\t\t\t};\r\n\t\t\t\t\t\t\t})(imageName, img));\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\timg.src = imgPath;\r\n\t\t\t\t\t\t\timgDict[imageName] = img;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tcount++;\r\n\t\t\t\t\t\t\tif (count === 15) // arbitrary\r\n\t\t\t\t\t\t\t\treturn false;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\treturn numberOfImagesLoaded === imageNamesArray.length;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar drawImageRotatedClockwise = function (imageName, x, y, degreesScaled, scalingFactorScaled) {\r\n\t\t\t\t\t\tif (canvas === null) {\r\n\t\t\t\t\t\t\tcanvas = document.getElementById('chessCompStompWithHacksCanvas');\r\n\t\t\t\t\t\t\tif (canvas !== null)\r\n\t\t\t\t\t\t\t\tcontext = canvas.getContext('2d');\r\n\t\t\t\t\t\t\telse\r\n\t\t\t\t\t\t\t\treturn;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar img = imgDict[imageName];\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (degreesScaled === 0 && scalingFactorScaled === 128) {\r\n\t\t\t\t\t\t\tcontext.drawImage(img, x, y);\r\n\t\t\t\t\t\t\treturn;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar scalingFactor = scalingFactorScaled / 128.0;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.translate(x, y);\r\n\t\t\t\t\t\tcontext.scale(scalingFactor, scalingFactor);\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.translate(img.width / 2, img.height / 2);\r\n\t\t\t\t\t\tcontext.rotate(degreesScaled * radianConversion);\r\n\t\t\t\t\t\tcontext.translate(-img.width / 2, -img.height / 2);\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.drawImage(img, 0, 0);\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.setTransform(1, 0, 0, 1, 0, 0);\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar getWidth = function (imageName) {\r\n\t\t\t\t\t\treturn widthDict[imageName];\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar getHeight = function (imageName) {\r\n\t\t\t\t\t\treturn heightDict[imageName];\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tloadImages: loadImages,\r\n\t\t\t\t\t\tdrawImageRotatedClockwise: drawImageRotatedClockwise,\r\n\t\t\t\t\t\tgetWidth: getWidth,\r\n\t\t\t\t\t\tgetHeight: getHeight\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            LoadImages: function () {
                var $t;
                var imageNames = "";
                var isFirst = true;

                $t = Bridge.getEnumerator(System.Enum.getValues(ChessCompStompWithHacksLibrary.ChessImage));
                try {
                    while ($t.moveNext()) {
                        var chessImage = Bridge.cast($t.Current, ChessCompStompWithHacksLibrary.ChessImage);
                        if (isFirst) {
                            isFirst = false;
                        } else {
                            imageNames = (imageNames || "") + ",";
                        }
                        imageNames = (imageNames || "") + (ChessCompStompWithHacksLibrary.ChessImageUtil.GetImageFilename(chessImage) || "");
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                if (Bridge.referenceEquals(imageNames, "")) {
                    return true;
                }

                var result = eval("window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.loadImages('" + (imageNames || "") + "')");

                if (result) {
                    return true;
                }
                return false;
            },
            DrawImageRotatedClockwise: function (image, x, y, degreesScaled, scalingFactorScaled) {
                var height = this.GetHeight(image);
                var scaledHeight = (Bridge.Int.div(Bridge.Int.mul(height, scalingFactorScaled), 128)) | 0;
                y = (((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT - y) | 0) - scaledHeight) | 0;

                window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.drawImageRotatedClockwise(ChessCompStompWithHacksLibrary.ChessImageUtil.GetImageFilename(image), x, y, degreesScaled, scalingFactorScaled);
            },
            GetWidth: function (image) {
                if (this.widthDictionary.containsKey(image)) {
                    return this.widthDictionary.get(image);
                }

                var width = this.GetWidthFromJavascript(image);
                this.widthDictionary.set(image, width);
                return width;
            },
            GetWidthFromJavascript: function (image) {
                return eval("window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.getWidth('" + (ChessCompStompWithHacksLibrary.ChessImageUtil.GetImageFilename(image) || "") + "')");
            },
            GetHeight: function (image) {
                if (this.heightDictionary.containsKey(image)) {
                    return this.heightDictionary.get(image);
                }

                var height = this.GetHeightFromJavascript(image);
                this.heightDictionary.set(image, height);
                return height;
            },
            GetHeightFromJavascript: function (image) {
                return eval("window.ChessCompStompWithHacksBridgeDisplayImagesJavascript.getHeight('" + (ChessCompStompWithHacksLibrary.ChessImageUtil.GetImageFilename(image) || "") + "')");
            }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeDisplayRectangle", {
        ctors: {
            ctor: function () {
                this.$initialize();
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeDisplayRectangleJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar canvas = null;\r\n\t\t\t\t\tvar context = null;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\tvar drawRectangle = function (x, y, width, height, red, green, blue, alpha, fill) {\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (canvas === null) {\r\n\t\t\t\t\t\t\tcanvas = document.getElementById('chessCompStompWithHacksCanvas');\t\t\r\n\t\t\t\t\t\t\tif (canvas === null)\r\n\t\t\t\t\t\t\t\treturn;\t\r\n\t\t\t\t\t\t\tcontext = canvas.getContext('2d');\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.fillStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';\r\n\t\t\t\t\t\tcontext.strokeStyle = 'rgba(' + red.toString() + ', ' + green.toString() + ', ' + blue.toString() + ', ' + (alpha / 255).toString() + ')';\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (fill)\r\n\t\t\t\t\t\t\tcontext.fillRect(x, y, width, height);\r\n\t\t\t\t\t\telse\r\n\t\t\t\t\t\t\tcontext.strokeRect(x, y, width, height);\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tdrawRectangle: drawRectangle\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            DrawRectangle: function (x, y, width, height, color, fill) {
                y = (((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT - y) | 0) - height) | 0;

                var red = color.R;
                var green = color.G;
                var blue = color.B;
                var alpha = color.Alpha;

                window.ChessCompStompWithHacksBridgeDisplayRectangleJavascript.drawRectangle(x, y, width, height, red, green, blue, alpha, fill);
            }
        }
    });

    Bridge.define("DTLibrary.IKeyboard", {
        $kind: "interface"
    });

    Bridge.define("DTLibrary.IMouse", {
        $kind: "interface"
    });

    Bridge.define("DTLibrary.IMusicCleanup", {
        $kind: "interface"
    });

    Bridge.define("DTLibrary.IMusicProcessing", {
        $kind: "interface"
    });

    Bridge.definei("DTLibrary.IMusicOutput$1", function (MusicEnum) { return {
        $kind: "interface"
    }; });

    Bridge.definei("DTLibrary.ISoundOutput$1", function (SoundEnum) { return {
        $kind: "interface"
    }; });

    Bridge.define("ChessCompStompWithHacks.ChessCompStompWithHacksInitializer", {
        statics: {
            fields: {
                bridgeKeyboard: null,
                bridgeMouse: null,
                previousKeyboard: null,
                previousMouse: null,
                display: null,
                soundOutput: null,
                music: null,
                frame: null,
                hasInitializedClearCanvasJavascript: false
            },
            methods: {
                InitializeClearCanvasJavascript: function () {
                    eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeClearCanvasJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar canvas = null;\r\n\t\t\t\t\tvar context = null;\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\tvar clearCanvas = function () {\r\n\t\t\t\t\t\tif (canvas === null) {\r\n\t\t\t\t\t\t\tcanvas = document.getElementById('chessCompStompWithHacksCanvas');\r\n\t\t\t\t\t\t\tif (canvas === null)\r\n\t\t\t\t\t\t\t\treturn;\t\r\n\t\t\t\t\t\t\tcontext = canvas.getContext('2d');\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tcontext.clearRect(0, 0, canvas.width, canvas.height);\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tclearCanvas: clearCanvas\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
                },
                ClearCanvas: function () {
                    if (!ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.hasInitializedClearCanvasJavascript) {
                        ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.InitializeClearCanvasJavascript();
                        ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.hasInitializedClearCanvasJavascript = true;
                    }

                    window.ChessCompStompWithHacksBridgeClearCanvasJavascript.clearCanvas();
                },
                Start: function (fps, debugMode) {
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.hasInitializedClearCanvasJavascript = false;

                    var logger;
                    if (debugMode) {
                        logger = new DTLibrary.ConsoleLogger();
                    } else {
                        logger = new DTLibrary.EmptyLogger();
                    }

                    var globalState = new ChessCompStompWithHacksLibrary.GlobalState(fps, new DTLibrary.DTRandom(), new DTLibrary.GuidGenerator("94197619109494365160"), logger, new DTLibrary.SimpleTimer(), true, debugMode, null, false);

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame = ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.GetFirstFrame(globalState);

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.bridgeKeyboard = new ChessCompStompWithHacks.BridgeKeyboard();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.bridgeMouse = new ChessCompStompWithHacks.BridgeMouse();

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.display = new ChessCompStompWithHacks.BridgeDisplay();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.soundOutput = new ChessCompStompWithHacks.BridgeSoundOutput(globalState.ElapsedMicrosPerFrame);
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.music = new ChessCompStompWithHacks.BridgeMusic();

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.previousKeyboard = new DTLibrary.EmptyKeyboard();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.previousMouse = new DTLibrary.EmptyMouse();

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.ClearCanvas();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render(ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.display);
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic(ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.music);
                },
                ProcessExtraTime: function (milliseconds) {
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime(milliseconds);
                },
                ComputeAndRenderNextFrame: function () {
                    var currentKeyboard = new DTLibrary.CopiedKeyboard(ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.bridgeKeyboard);
                    var currentMouse = new DTLibrary.CopiedMouse(ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.bridgeMouse);

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame = ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame(currentKeyboard, currentMouse, ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.previousKeyboard, ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.previousMouse, ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.display, ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.soundOutput, ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.music);
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.soundOutput.DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$ProcessFrame();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.ClearCanvas();
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render(ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.display);
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.frame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic(ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.music);

                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.previousKeyboard = new DTLibrary.CopiedKeyboard(currentKeyboard);
                    ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.previousMouse = new DTLibrary.CopiedMouse(currentMouse);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacks.Program", {
        main: function Main (args) {
            ChessCompStompWithHacks.Program.AddFpsDisplayJavascript();
            ChessCompStompWithHacks.Program.Initialize();
        },
        statics: {
            methods: {
                AddFpsDisplayJavascript: function () {
                    eval("\n\t\t\t\twindow.ChessCompStompWithHacksFpsDisplayJavascript = ((function () {\n\t\t\t\t\t'use strict';\n\t\t\t\t\t\n\t\t\t\t\tvar numberOfFrames = 0;\n\t\t\t\t\tvar hasAddedFpsLabel = false;\n\t\t\t\t\tvar startTimeMillis = Date.now();\n\t\t\t\t\tvar fpsNode = null;\n\t\t\t\t\t\n\t\t\t\t\tvar frameComputedAndRendered = function () {\n\t\t\t\t\t\tnumberOfFrames++;\n\t\t\t\t\t};\n\t\t\t\t\t\n\t\t\t\t\tvar displayFps = function () {\n\t\t\t\t\t\tif (!hasAddedFpsLabel) {\n\t\t\t\t\t\t\tvar fpsLabelNode = document.getElementById('chessCompStompWithHacksFpsLabel');\n\t\t\t\t\t\t\tif (fpsLabelNode !== null) {\n\t\t\t\t\t\t\t\tfpsLabelNode.textContent = 'FPS: ';\n\t\t\t\t\t\t\t\thasAddedFpsLabel = true;\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t}\n\t\t\t\t\t\t\n\t\t\t\t\t\tvar currentTimeMillis = Date.now();\n\t\t\t\t\t\tif (currentTimeMillis - startTimeMillis > 2000) {\n\t\t\t\t\t\t\tvar actualFps = numberOfFrames / 2;\n\t\t\t\t\t\t\t\n\t\t\t\t\t\t\tif (fpsNode === null)\n\t\t\t\t\t\t\t\tfpsNode = document.getElementById('chessCompStompWithHacksFps');\n\t\t\t\t\t\t\t\n\t\t\t\t\t\t\tif (fpsNode !== null)\n\t\t\t\t\t\t\t\tfpsNode.textContent = actualFps.toString();\n\t\t\t\t\t\t\t\n\t\t\t\t\t\t\tnumberOfFrames = 0;\n\t\t\t\t\t\t\tstartTimeMillis = currentTimeMillis;\n\t\t\t\t\t\t}\n\t\t\t\t\t};\n\t\t\t\t\t\n\t\t\t\t\treturn {\n\t\t\t\t\t\tframeComputedAndRendered: frameComputedAndRendered,\n\t\t\t\t\t\tdisplayFps: displayFps\n\t\t\t\t\t};\n\t\t\t\t})());\n\t\t\t");
                },
                Initialize: function () {
                    eval("\n\t\t\t\t((function () {\n\t\t\t\t\t'use strict';\n\t\t\t\t\t\n\t\t\t\t\tvar urlParams = (new URL(document.location)).searchParams;\n\t\t\t\t\t\n\t\t\t\t\tvar showFps = urlParams.get('showfps') !== null\n\t\t\t\t\t\t? (urlParams.get('showfps') === 'true')\n\t\t\t\t\t\t: false;\n\t\t\t\t\tvar fps = urlParams.get('fps') !== null\n\t\t\t\t\t\t? parseInt(urlParams.get('fps'), 10)\n\t\t\t\t\t\t: 60;\n\t\t\t\t\tvar debugMode = urlParams.get('debugmode') !== null\n\t\t\t\t\t\t? (urlParams.get('debugmode') === 'true')\n\t\t\t\t\t\t: false;\n\t\t\t\t\t\n\t\t\t\t\twindow.ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.Start(fps, debugMode);\n\t\t\t\t\t\n\t\t\t\t\tvar computeAndRenderNextFrame;\n\t\t\t\t\t\n\t\t\t\t\tvar nextTimeToAct = Date.now() + (1000.0 / fps);\n\t\t\t\t\t\n\t\t\t\t\tvar hasProcessedExtraTime = false;\n\t\t\t\t\t\n\t\t\t\t\tcomputeAndRenderNextFrame = function () {\n\t\t\t\t\t\tvar now = Date.now();\n\t\t\t\t\t\t\n\t\t\t\t\t\tif (nextTimeToAct > now) {\n\t\t\t\t\t\t\tif (!hasProcessedExtraTime) {\n\t\t\t\t\t\t\t\tvar extraTime = Math.round(nextTimeToAct - now);\n\t\t\t\t\t\t\t\tif (extraTime > 0)\n\t\t\t\t\t\t\t\t\twindow.ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.ProcessExtraTime(extraTime);\n\t\t\t\t\t\t\t\thasProcessedExtraTime = true;\n\t\t\t\t\t\t\t\tsetTimeout(computeAndRenderNextFrame, 0);\n\t\t\t\t\t\t\t} else {\n\t\t\t\t\t\t\t\tsetTimeout(computeAndRenderNextFrame, 5);\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\treturn;\n\t\t\t\t\t\t}\n\t\t\t\t\t\t\n\t\t\t\t\t\thasProcessedExtraTime = false;\n\t\t\t\t\t\t\n\t\t\t\t\t\tif (nextTimeToAct < now - 5.0*(1000.0 / fps))\n\t\t\t\t\t\t\tnextTimeToAct = now - 5.0*(1000.0 / fps);\n\t\t\t\t\t\t\n\t\t\t\t\t\tnextTimeToAct = nextTimeToAct + (1000.0 / fps);\n\t\t\t\t\t\t\n\t\t\t\t\t\twindow.ChessCompStompWithHacks.ChessCompStompWithHacksInitializer.ComputeAndRenderNextFrame();\n\t\t\t\t\t\twindow.ChessCompStompWithHacksFpsDisplayJavascript.frameComputedAndRendered();\n\t\t\t\t\t\t\n\t\t\t\t\t\tif (showFps)\n\t\t\t\t\t\t\twindow.ChessCompStompWithHacksFpsDisplayJavascript.displayFps();\n\t\t\t\t\t\t\n\t\t\t\t\t\tsetTimeout(computeAndRenderNextFrame, 0);\n\t\t\t\t\t};\n\t\t\t\t\t\n\t\t\t\t\tsetTimeout(computeAndRenderNextFrame, 0);\n\t\t\t\t})());\n\t\t\t");
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.IChessAI", {
        $kind: "interface"
    });

    Bridge.define("ChessCompStompWithHacksEngine.CheckKingUnderAttack", {
        statics: {
            methods: {
                /**
                 * Note that this method is used (in part) to determine whether a move is legal.
                 So the board input may represent an invalid board position; e.g. maybe both
                 kings are in check, or the kings are adjacent to each other, etc.
                 *
                 * @static
                 * @public
                 * @this ChessCompStompWithHacksEngine.CheckKingUnderAttack
                 * @memberof ChessCompStompWithHacksEngine.CheckKingUnderAttack
                 * @param   {ChessCompStompWithHacksEngine.ChessSquarePieceArray}        board                        
                 * @param   {ChessCompStompWithHacksEngine.GameState.PlayerAbilities}    playerAbilities              
                 * @param   {boolean}                                                    checkWhiteKingUnderAttack    
                 * @param   {boolean}                                                    isPlayerWhite                
                 * @param   {number}                                                     kingFile                     
                 * @param   {number}                                                     kingRank
                 * @return  {boolean}
                 */
                IsKingUnderThreat: function (board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank) {
                    var isKingAttackedByPawn = ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreatByPawn(board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank);

                    if (isKingAttackedByPawn) {
                        return true;
                    }

                    var isKingAttackedFromEightDirections = ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreatFromEightDirections(board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank);

                    if (isKingAttackedFromEightDirections) {
                        return true;
                    }

                    var isKingAttackedViaKnightMove = ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreatByKnightOrLargeKnightMove(board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank);

                    if (isKingAttackedViaKnightMove) {
                        return true;
                    }

                    return false;
                },
                IsKingUnderThreatByPawn: function (board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank) {
                    if (checkWhiteKingUnderAttack) {
                        var pawnFile = (kingFile - 1) | 0;
                        var pawnRank = (kingRank + 1) | 0;

                        if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank) && board.GetPiece$1(pawnFile, pawnRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn) {
                            return true;
                        }

                        pawnFile = (kingFile + 1) | 0;
                        pawnRank = (kingRank + 1) | 0;

                        if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank) && board.GetPiece$1(pawnFile, pawnRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn) {
                            return true;
                        }

                        if (!isPlayerWhite && playerAbilities.CanSuperEnPassant) {
                            pawnFile = (kingFile - 1) | 0;
                            pawnRank = kingRank;
                            if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank) && board.GetPiece$1(pawnFile, pawnRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(((kingRank - 1) | 0)) && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(board.GetPiece$1(kingFile, ((kingRank - 1) | 0))) === false) {
                                return true;
                            }

                            pawnFile = (kingFile + 1) | 0;
                            pawnRank = kingRank;
                            if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank) && board.GetPiece$1(pawnFile, pawnRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(((kingRank - 1) | 0)) && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(board.GetPiece$1(kingFile, ((kingRank - 1) | 0))) === false) {
                                return true;
                            }
                        }
                    } else {
                        var pawnFile1 = (kingFile - 1) | 0;
                        var pawnRank1 = (kingRank - 1) | 0;

                        if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile1) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank1) && board.GetPiece$1(pawnFile1, pawnRank1) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn) {
                            return true;
                        }

                        pawnFile1 = (kingFile + 1) | 0;
                        pawnRank1 = (kingRank - 1) | 0;

                        if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile1) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank1) && board.GetPiece$1(pawnFile1, pawnRank1) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn) {
                            return true;
                        }

                        if (isPlayerWhite && playerAbilities.CanSuperEnPassant) {
                            pawnFile1 = (kingFile - 1) | 0;
                            pawnRank1 = kingRank;
                            if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile1) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank1) && board.GetPiece$1(pawnFile1, pawnRank1) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(((kingRank + 1) | 0)) && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(board.GetPiece$1(kingFile, ((kingRank + 1) | 0))) === false) {
                                return true;
                            }

                            pawnFile1 = (kingFile + 1) | 0;
                            pawnRank1 = kingRank;
                            if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnFile1) && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(pawnRank1) && board.GetPiece$1(pawnFile1, pawnRank1) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn && ChessCompStompWithHacksEngine.CheckKingUnderAttack.InRange(((kingRank + 1) | 0)) && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(board.GetPiece$1(kingFile, ((kingRank + 1) | 0))) === false) {
                                return true;
                            }
                        }
                    }

                    return false;
                },
                IsKingUnderThreatFromEightDirections: function (board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank) {
                    var $t, $t1;
                    var shouldCheckRookCannon = playerAbilities.CanRooksCaptureLikeCannons && (isPlayerWhite && !checkWhiteKingUnderAttack || !isPlayerWhite && checkWhiteKingUnderAttack);

                    var enemyKing = checkWhiteKingUnderAttack ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing;
                    var enemyQueen = checkWhiteKingUnderAttack ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                    var enemyRook = checkWhiteKingUnderAttack ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                    var enemyBishop = checkWhiteKingUnderAttack ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;

                    var piecesThatAttackHorizontallyOrVertically = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ChessSquarePiece)).ctor();
                    piecesThatAttackHorizontallyOrVertically.add(enemyRook);
                    piecesThatAttackHorizontallyOrVertically.add(enemyQueen);

                    var piecesThatAttackDiagonally = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ChessSquarePiece)).ctor();
                    piecesThatAttackDiagonally.add(enemyBishop);
                    piecesThatAttackDiagonally.add(enemyQueen);
                    if (playerAbilities.CanRooksMoveLikeBishops && (isPlayerWhite && !checkWhiteKingUnderAttack || !isPlayerWhite && checkWhiteKingUnderAttack)) {
                        piecesThatAttackDiagonally.add(enemyRook);
                    }

                    var deltas = new (System.Collections.Generic.List$1(System.Tuple$3(System.Int32,System.Int32,System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ChessSquarePiece)))).ctor();
                    deltas.add({ Item1: 1, Item2: 0, Item3: piecesThatAttackHorizontallyOrVertically });
                    deltas.add({ Item1: -1, Item2: 0, Item3: piecesThatAttackHorizontallyOrVertically });
                    deltas.add({ Item1: 0, Item2: 1, Item3: piecesThatAttackHorizontallyOrVertically });
                    deltas.add({ Item1: 0, Item2: -1, Item3: piecesThatAttackHorizontallyOrVertically });
                    deltas.add({ Item1: 1, Item2: 1, Item3: piecesThatAttackDiagonally });
                    deltas.add({ Item1: 1, Item2: -1, Item3: piecesThatAttackDiagonally });
                    deltas.add({ Item1: -1, Item2: 1, Item3: piecesThatAttackDiagonally });
                    deltas.add({ Item1: -1, Item2: -1, Item3: piecesThatAttackDiagonally });

                    $t = Bridge.getEnumerator(deltas);
                    try {
                        while ($t.moveNext()) {
                            var delta = $t.Current;
                            var i = kingFile;
                            var j = kingRank;
                            var isFirstIteration = true;
                            while (true) {
                                i = (i + delta.Item1) | 0;
                                j = (j + delta.Item2) | 0;

                                if (i < 0 || i >= 8 || j < 0 || j >= 8) {
                                    break;
                                }

                                var pieceAtThisSquare = board.GetPiece$1(i, j);
                                if (pieceAtThisSquare !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    $t1 = Bridge.getEnumerator(delta.Item3);
                                    try {
                                        while ($t1.moveNext()) {
                                            var enemyPiece = $t1.Current;
                                            if (pieceAtThisSquare === enemyPiece) {
                                                return true;
                                            }
                                        }
                                    } finally {
                                        if (Bridge.is($t1, System.IDisposable)) {
                                            $t1.System$IDisposable$Dispose();
                                        }
                                    }

                                    if (isFirstIteration && pieceAtThisSquare === enemyKing) {
                                        return true;
                                    }
                                }

                                isFirstIteration = false;

                                if (pieceAtThisSquare !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    if (shouldCheckRookCannon && delta.Item3.contains(enemyRook)) {
                                        while (true) {
                                            i = (i + delta.Item1) | 0;
                                            j = (j + delta.Item2) | 0;
                                            if (i < 0 || i >= 8 || j < 0 || j >= 8) {
                                                break;
                                            }

                                            if (board.GetPiece$1(i, j) === enemyRook) {
                                                return true;
                                            }

                                            if (board.GetPiece$1(i, j) !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                                break;
                                            }
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    return false;
                },
                IsKingUnderThreatByKnightOrLargeKnightMove: function (board, playerAbilities, checkWhiteKingUnderAttack, isPlayerWhite, kingFile, kingRank) {
                    var $t;
                    var deltas = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    deltas.add({ Item1: 2, Item2: 1 });
                    deltas.add({ Item1: 1, Item2: 2 });
                    deltas.add({ Item1: 2, Item2: -1 });
                    deltas.add({ Item1: 1, Item2: -2 });
                    deltas.add({ Item1: -2, Item2: 1 });
                    deltas.add({ Item1: -1, Item2: 2 });
                    deltas.add({ Item1: -2, Item2: -1 });
                    deltas.add({ Item1: -1, Item2: -2 });

                    if (playerAbilities.CanKnightsMakeLargeKnightsMove && (checkWhiteKingUnderAttack && !isPlayerWhite || !checkWhiteKingUnderAttack && isPlayerWhite)) {
                        deltas.add({ Item1: 3, Item2: 1 });
                        deltas.add({ Item1: 1, Item2: 3 });
                        deltas.add({ Item1: 3, Item2: -1 });
                        deltas.add({ Item1: 1, Item2: -3 });
                        deltas.add({ Item1: -3, Item2: 1 });
                        deltas.add({ Item1: -1, Item2: 3 });
                        deltas.add({ Item1: -3, Item2: -1 });
                        deltas.add({ Item1: -1, Item2: -3 });
                    }

                    var enemyKnight = checkWhiteKingUnderAttack ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                    var enemyQueen = checkWhiteKingUnderAttack ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;

                    $t = Bridge.getEnumerator(deltas);
                    try {
                        while ($t.moveNext()) {
                            var delta = $t.Current;
                            var i = (kingFile + delta.Item1) | 0;
                            var j = (kingRank + delta.Item2) | 0;

                            if (i < 0 || i >= 8 || j < 0 || j >= 8) {
                                continue;
                            }

                            if (board.GetPiece$1(i, j) === enemyKnight) {
                                return true;
                            }

                            if (playerAbilities.CanQueensMoveLikeKnights && (checkWhiteKingUnderAttack && !isPlayerWhite || !checkWhiteKingUnderAttack && isPlayerWhite) && board.GetPiece$1(i, j) === enemyQueen) {
                                return true;
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    return false;
                },
                InRange: function (x) {
                    return 0 <= x && x < 8;
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ChessSquare", {
        inherits: function () { return [System.IEquatable$1(ChessCompStompWithHacksEngine.ChessSquare)]; },
        fields: {
            File: 0,
            Rank: 0
        },
        alias: ["equalsT", "System$IEquatable$1$ChessCompStompWithHacksEngine$ChessSquare$equalsT"],
        ctors: {
            ctor: function (file, rank) {
                this.$initialize();
                this.File = file;
                this.Rank = rank;
            }
        },
        methods: {
            equals: function (obj) {
                return this.equalsT(Bridge.as(obj, ChessCompStompWithHacksEngine.ChessSquare));
            },
            equalsT: function (other) {
                return other != null && this.File === other.File && this.Rank === other.Rank;
            },
            getHashCode: function () {
                return (((this.File << 3) + this.Rank) | 0);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ChessSquarePiece", {
        $kind: "enum",
        statics: {
            fields: {
                BlackPawn: 0,
                BlackKnight: 1,
                BlackBishop: 2,
                BlackRook: 3,
                BlackQueen: 4,
                BlackKing: 5,
                WhitePawn: 6,
                WhiteKnight: 7,
                WhiteBishop: 8,
                WhiteRook: 9,
                WhiteQueen: 10,
                WhiteKing: 11,
                Empty: 12
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ChessSquarePieceArray", {
        statics: {
            methods: {
                EmptyBoard: function () {
                    var $t;
                    var returnValue = new ChessCompStompWithHacksEngine.ChessSquarePieceArray.ctor();

                    returnValue.board = System.Array.init(8, null, System.Array.type(ChessCompStompWithHacksEngine.ChessSquarePiece));
                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        returnValue.board[System.Array.index(i, returnValue.board)] = System.Array.init(8, 0, ChessCompStompWithHacksEngine.ChessSquarePiece);
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            ($t = returnValue.board[System.Array.index(i, returnValue.board)])[System.Array.index(j, $t)] = ChessCompStompWithHacksEngine.ChessSquarePiece.Empty;
                        }
                    }

                    return returnValue;
                },
                CopyBoard: function (board) {
                    var $t, $t1;
                    var newBoard = System.Array.init(8, null, System.Array.type(ChessCompStompWithHacksEngine.ChessSquarePiece));
                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        newBoard[System.Array.index(i, newBoard)] = System.Array.init(8, 0, ChessCompStompWithHacksEngine.ChessSquarePiece);
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            ($t = newBoard[System.Array.index(i, newBoard)])[System.Array.index(j, $t)] = ($t1 = board[System.Array.index(i, board)])[System.Array.index(j, $t1)];
                        }
                    }

                    return newBoard;
                }
            }
        },
        fields: {
            board: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
            },
            $ctor1: function (board) {
                this.$initialize();
                this.board = ChessCompStompWithHacksEngine.ChessSquarePieceArray.CopyBoard(board);
            }
        },
        methods: {
            GetPiece$1: function (file, rank) {
                var $t;
                return ($t = this.board[System.Array.index(file, this.board)])[System.Array.index(rank, $t)];
            },
            GetPiece: function (chessSquare) {
                var $t;
                return ($t = this.board[System.Array.index(chessSquare.File, this.board)])[System.Array.index(chessSquare.Rank, $t)];
            },
            SetPiece: function (file, rank, piece) {
                var $t, $t1, $t2;
                var newBoard = System.Array.init(8, null, System.Array.type(ChessCompStompWithHacksEngine.ChessSquarePiece));

                for (var i = 0; i < 8; i = (i + 1) | 0) {
                    if (i !== file) {
                        newBoard[System.Array.index(i, newBoard)] = this.board[System.Array.index(i, this.board)];
                    } else {
                        newBoard[System.Array.index(i, newBoard)] = System.Array.init(8, 0, ChessCompStompWithHacksEngine.ChessSquarePiece);
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            ($t = newBoard[System.Array.index(i, newBoard)])[System.Array.index(j, $t)] = ($t1 = this.board[System.Array.index(i, this.board)])[System.Array.index(j, $t1)];
                        }
                        ($t2 = newBoard[System.Array.index(i, newBoard)])[System.Array.index(rank, $t2)] = piece;
                    }
                }

                var returnValue = new ChessCompStompWithHacksEngine.ChessSquarePieceArray.ctor();
                returnValue.board = newBoard;

                return returnValue;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ChessSquarePieceUtil", {
        statics: {
            methods: {
                IsPawn: function (piece) {
                    return piece === ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn || piece === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                },
                IsRook: function (piece) {
                    return piece === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook || piece === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                },
                IsKnight: function (piece) {
                    return piece === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight || piece === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                },
                IsBishop: function (piece) {
                    return piece === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop || piece === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                },
                IsQueen: function (piece) {
                    return piece === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen || piece === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                },
                IsKing: function (piece) {
                    return piece === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing || piece === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing;
                },
                IsWhite: function (piece) {
                    switch (piece) {
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing: 
                            return false;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen: 
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing: 
                            return true;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.Empty: 
                            return false;
                        default: 
                            throw new System.Exception();
                    }
                },
                IsBlack: function (piece) {
                    if (piece === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                        return false;
                    }

                    return !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMoves", {
        statics: {
            methods: {
                GetMoves: function (gameState) {
                    var $t;
                    var kingFile = -1;
                    var kingRank = -1;

                    var king = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing;

                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            if (gameState.Board.GetPiece$1(i, j) === king) {
                                kingFile = i;
                                kingRank = j;
                                break;
                            }
                        }

                        if (kingFile !== -1) {
                            break;
                        }
                    }

                    if (kingFile === -1) {
                        throw new System.Exception();
                    }

                    var moves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo)).ctor();

                    // Populate moves by looking at all 64 squares and seeing what moves are available
                    for (var i1 = 0; i1 < 8; i1 = (i1 + 1) | 0) {
                        for (var j1 = 0; j1 < 8; j1 = (j1 + 1) | 0) {
                            var piece = gameState.Board.GetPiece$1(i1, j1);

                            if (gameState.IsWhiteTurn) {
                                if (!ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece)) {
                                    continue;
                                }
                            } else {
                                if (!ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(piece)) {
                                    continue;
                                }
                            }

                            switch (piece) {
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn: 
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn: 
                                    ChessCompStompWithHacksEngine.ComputeMovesPawn.AddPawnMoves(gameState, i1, j1, kingFile, kingRank, moves);
                                    break;
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook: 
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook: 
                                    ChessCompStompWithHacksEngine.ComputeMovesRook.AddRookMoves(gameState, i1, j1, kingFile, kingRank, moves);
                                    break;
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight: 
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight: 
                                    ChessCompStompWithHacksEngine.ComputeMovesKnight.AddKnightMoves(gameState, i1, j1, kingFile, kingRank, moves);
                                    break;
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop: 
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop: 
                                    ChessCompStompWithHacksEngine.ComputeMovesBishop.AddBishopMoves(gameState, i1, j1, kingFile, kingRank, moves);
                                    break;
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen: 
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen: 
                                    ChessCompStompWithHacksEngine.ComputeMovesQueen.AddQueenMoves(gameState, i1, j1, kingFile, kingRank, moves);
                                    break;
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing: 
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing: 
                                    ChessCompStompWithHacksEngine.ComputeMovesKing.AddKingMoves(gameState, i1, j1, moves);
                                    break;
                                case ChessCompStompWithHacksEngine.ChessSquarePiece.Empty: 
                                    break;
                                default: 
                                    throw new System.Exception();
                            }
                        }
                    }

                    ChessCompStompWithHacksEngine.ComputeMovesTacticalNuke.AddTacticalNukeMoves(gameState, kingFile, kingRank, moves);

                    if (!gameState.IsPlayerTurn() && gameState.Abilities.HasOpponentMustCaptureWhenPossible) {
                        var captureMoves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo)).ctor();
                        $t = Bridge.getEnumerator(moves);
                        try {
                            while ($t.moveNext()) {
                                var move = $t.Current;
                                if (move.IsCaptureMove) {
                                    captureMoves.add(move);
                                }
                            }
                        } finally {
                            if (Bridge.is($t, System.IDisposable)) {
                                $t.System$IDisposable$Dispose();
                            }
                        }

                        if (captureMoves.Count > 0) {
                            moves = captureMoves;
                        }
                    }

                    var gameStatus = new ChessCompStompWithHacksEngine.ComputeMoves.GameStatus();

                    if (moves.Count > 0) {
                        gameStatus = ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.InProgress;
                    } else {
                        var isKingUnderThreat = ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(gameState.Board, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, kingFile, kingRank);

                        if (isKingUnderThreat) {
                            gameStatus = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory : ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory;
                        } else {
                            if (gameState.Abilities.HasStalemateIsVictory) {
                                gameStatus = gameState.IsPlayerWhite ? ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory : ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory;
                            } else {
                                gameStatus = ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.Stalemate;
                            }
                        }
                    }

                    var returnValue = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Move)).$ctor2(moves.Count);
                    for (var i2 = 0; i2 < moves.Count; i2 = (i2 + 1) | 0) {
                        returnValue.add(moves.getItem(i2).Move);
                    }

                    return new ChessCompStompWithHacksEngine.ComputeMoves.Result(returnValue, gameStatus);
                },
                AddMoveInfosForNonEnPassantNonCastlingMoves: function (startingFile, startingRank, endingFile, endingRank, piece, gameState, kingFile, kingRank, moves) {
                    var isCaptureMove = gameState.Board.GetPiece$1(endingFile, endingRank) !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty;

                    var shouldDestroyPiece = isCaptureMove && !gameState.IsPlayerTurn() && gameState.Abilities.HasPawnsDestroyCapturingPiece && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(gameState.Board.GetPiece$1(endingFile, endingRank));

                    var newBoard = gameState.Board.SetPiece(endingFile, endingRank, shouldDestroyPiece ? ChessCompStompWithHacksEngine.ChessSquarePiece.Empty : (gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook)).SetPiece(startingFile, startingRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);

                    if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(newBoard, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, kingFile, kingRank)) {
                        return;
                    }

                    if (gameState.IsWhiteTurn) {
                        var isPromotion;

                        if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(piece) && endingRank === 7) {
                            isPromotion = true;
                        } else {
                            if (gameState.IsPlayerWhite && gameState.Abilities.HasAnyPieceCanPromote && (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsRook(piece) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKnight(piece) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBishop(piece) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsQueen(piece)) && endingRank === 7) {
                                isPromotion = true;
                            } else {
                                isPromotion = false;
                            }
                        }

                        if (isPromotion) {
                            ChessCompStompWithHacksEngine.ComputeMoves.AddPromotionMoveInfos(startingFile, startingRank, endingFile, endingRank, isCaptureMove, moves);
                        } else {
                            moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.NormalMove(startingFile, startingRank, endingFile, endingRank), isCaptureMove));
                        }
                    } else {
                        var isPromotion1;

                        if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(piece) && endingRank === 0) {
                            isPromotion1 = true;
                        } else {
                            if (!gameState.IsPlayerWhite && gameState.Abilities.HasAnyPieceCanPromote && (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsRook(piece) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKnight(piece) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBishop(piece) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsQueen(piece)) && endingRank === 0) {
                                isPromotion1 = true;
                            } else {
                                isPromotion1 = false;
                            }
                        }

                        if (isPromotion1) {
                            ChessCompStompWithHacksEngine.ComputeMoves.AddPromotionMoveInfos(startingFile, startingRank, endingFile, endingRank, isCaptureMove, moves);
                        } else {
                            moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.NormalMove(startingFile, startingRank, endingFile, endingRank), isCaptureMove));
                        }
                    }
                },
                AddPromotionMoveInfos: function (startingFile, startingRank, endingFile, endingRank, isCaptureMove, moves) {
                    moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.PromotionMove(startingFile, startingRank, endingFile, endingRank, ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToRook), isCaptureMove));
                    moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.PromotionMove(startingFile, startingRank, endingFile, endingRank, ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToKnight), isCaptureMove));
                    moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.PromotionMove(startingFile, startingRank, endingFile, endingRank, ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToBishop), isCaptureMove));
                    moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.PromotionMove(startingFile, startingRank, endingFile, endingRank, ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen), isCaptureMove));
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMoves.GameStatus", {
        $kind: "nested enum",
        statics: {
            fields: {
                InProgress: 0,
                Stalemate: 1,
                WhiteVictory: 2,
                BlackVictory: 3
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo", {
        $kind: "nested class",
        fields: {
            Move: null,
            IsCaptureMove: false
        },
        ctors: {
            ctor: function (move, isCaptureMove) {
                this.$initialize();
                this.Move = move;
                this.IsCaptureMove = isCaptureMove;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMoves.Result", {
        $kind: "nested class",
        fields: {
            Moves: null,
            GameStatus: 0
        },
        ctors: {
            ctor: function (moves, gameStatus) {
                this.$initialize();
                this.Moves = moves;
                this.GameStatus = gameStatus;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesBishop", {
        statics: {
            methods: {
                AddBishopMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    var $t;
                    var piece = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;

                    var deltas = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    deltas.add({ Item1: 1, Item2: 1 });
                    deltas.add({ Item1: 1, Item2: -1 });
                    deltas.add({ Item1: -1, Item2: 1 });
                    deltas.add({ Item1: -1, Item2: -1 });

                    $t = Bridge.getEnumerator(deltas);
                    try {
                        while ($t.moveNext()) {
                            var delta = $t.Current;
                            var endI = i;
                            var endJ = j;
                            while (true) {
                                endI = (endI + delta.Item1) | 0;
                                endJ = (endJ + delta.Item2) | 0;
                                if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8) {
                                    break;
                                }

                                var pieceAtDestination = gameState.Board.GetPiece$1(endI, endJ);

                                if (gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(pieceAtDestination) || !gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(pieceAtDestination)) {
                                    ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, endI, endJ, piece, gameState, kingFile, kingRank, moves);
                                }

                                if (pieceAtDestination !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    break;
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesKing", {
        statics: {
            methods: {
                AddKingMoves: function (gameState, i, j, moves) {
                    ChessCompStompWithHacksEngine.ComputeMovesKing.AddNonCastlingKingMoves(gameState, i, j, moves);

                    var canSuperCastle = gameState.IsPlayerTurn() && gameState.Abilities.CanSuperCastle;

                    if (canSuperCastle) {
                        ChessCompStompWithHacksEngine.ComputeMovesKing.AddSuperCastlingKingMoves(gameState, i, j, moves);
                    } else {
                        ChessCompStompWithHacksEngine.ComputeMovesKing.AddNormalCastlingKingMoves(gameState, i, j, moves);
                    }
                },
                AddNormalCastlingKingMoves: function (gameState, i, j, moves) {
                    var friendlyKing = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing;
                    var friendlyRook = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;

                    var backRank = gameState.IsWhiteTurn ? 0 : 7;

                    var canNormalCastleKingside = gameState.IsWhiteTurn ? gameState.Castling.CanWhiteCastleKingside : gameState.Castling.CanBlackCastleKingside;

                    var canNormalCastleQueenside = gameState.IsWhiteTurn ? gameState.Castling.CanWhiteCastleQueenside : gameState.Castling.CanBlackCastleQueenside;

                    if (canNormalCastleKingside) {
                        if (gameState.Board.GetPiece$1(4, backRank) === friendlyKing && gameState.Board.GetPiece$1(5, backRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && gameState.Board.GetPiece$1(6, backRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && gameState.Board.GetPiece$1(7, backRank) === friendlyRook) {
                            var interimBoard = gameState.Board.SetPiece(4, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(5, backRank, friendlyKing);
                            var newBoard = gameState.Board.SetPiece(4, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(5, backRank, friendlyRook).SetPiece(6, backRank, friendlyKing).SetPiece(7, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                            ChessCompStompWithHacksEngine.ComputeMovesKing.GetMoveInfosForCastlingAndSuperCastlingMoves(i, j, 6, backRank, interimBoard, newBoard, gameState, moves);
                        }
                    }

                    if (canNormalCastleQueenside) {
                        if (gameState.Board.GetPiece$1(4, backRank) === friendlyKing && gameState.Board.GetPiece$1(3, backRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && gameState.Board.GetPiece$1(2, backRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && gameState.Board.GetPiece$1(1, backRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && gameState.Board.GetPiece$1(0, backRank) === friendlyRook) {
                            var interimBoard1 = gameState.Board.SetPiece(4, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(3, backRank, friendlyKing);
                            var newBoard1 = gameState.Board.SetPiece(4, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(3, backRank, friendlyRook).SetPiece(2, backRank, friendlyKing).SetPiece(1, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(0, backRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                            ChessCompStompWithHacksEngine.ComputeMovesKing.GetMoveInfosForCastlingAndSuperCastlingMoves(i, j, 2, backRank, interimBoard1, newBoard1, gameState, moves);
                        }
                    }
                },
                AddSuperCastlingKingMoves: function (gameState, i, j, moves) {
                    var $t;
                    var friendlyKing = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing;
                    var friendlyRook = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;

                    var deltas = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    if (((i + 2) | 0) < 8) {
                        deltas.add({ Item1: 1, Item2: 0 });
                    }
                    if (((i - 2) | 0) >= 0) {
                        deltas.add({ Item1: -1, Item2: 0 });
                    }
                    if (((j + 2) | 0) < 8) {
                        deltas.add({ Item1: 0, Item2: 1 });
                    }
                    if (((j - 2) | 0) >= 0) {
                        deltas.add({ Item1: 0, Item2: -1 });
                    }

                    $t = Bridge.getEnumerator(deltas);
                    try {
                        while ($t.moveNext()) {
                            var delta = $t.Current;
                            var kingI = i;
                            var kingJ = j;
                            var count = 0;
                            while (true) {
                                kingI = (kingI + delta.Item1) | 0;
                                kingJ = (kingJ + delta.Item2) | 0;
                                count = (count + 1) | 0;

                                if (kingI < 0 || kingI >= 8 || kingJ < 0 || kingJ >= 8) {
                                    break;
                                }

                                if (gameState.Board.GetPiece$1(kingI, kingJ) === friendlyRook) {
                                    if (count !== 1 || gameState.Board.GetPiece$1(((((i + delta.Item1) | 0) + delta.Item1) | 0), ((((j + delta.Item2) | 0) + delta.Item2) | 0)) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                        var interimBoard = gameState.Board.SetPiece(i, j, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(((i + delta.Item1) | 0), ((j + delta.Item2) | 0), friendlyKing);
                                        var newBoard = gameState.Board.SetPiece(i, j, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(kingI, kingJ, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(((i + delta.Item1) | 0), ((j + delta.Item2) | 0), friendlyRook).SetPiece(((((i + delta.Item1) | 0) + delta.Item1) | 0), ((((j + delta.Item2) | 0) + delta.Item2) | 0), friendlyKing);

                                        ChessCompStompWithHacksEngine.ComputeMovesKing.GetMoveInfosForCastlingAndSuperCastlingMoves(i, j, ((((i + delta.Item1) | 0) + delta.Item1) | 0), ((((j + delta.Item2) | 0) + delta.Item2) | 0), interimBoard, newBoard, gameState, moves);
                                    }
                                    break;
                                }

                                if (gameState.Board.GetPiece$1(kingI, kingJ) !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    break;
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                },
                AddNonCastlingKingMoves: function (gameState, i, j, moves) {
                    var $t;
                    var piece = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing;

                    var kingMoves = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    kingMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 1) | 0) });
                    kingMoves.add({ Item1: ((i - 1) | 0), Item2: j });
                    kingMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 1) | 0) });
                    kingMoves.add({ Item1: i, Item2: ((j - 1) | 0) });
                    kingMoves.add({ Item1: i, Item2: ((j + 1) | 0) });
                    kingMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 1) | 0) });
                    kingMoves.add({ Item1: ((i + 1) | 0), Item2: j });
                    kingMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 1) | 0) });

                    $t = Bridge.getEnumerator(kingMoves);
                    try {
                        while ($t.moveNext()) {
                            var kingMove = $t.Current;
                            if (0 <= kingMove.Item1 && kingMove.Item1 < 8 && 0 <= kingMove.Item2 && kingMove.Item2 < 8) {
                                var pieceAtDestination = gameState.Board.GetPiece$1(kingMove.Item1, kingMove.Item2);

                                if (gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(pieceAtDestination) || !gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(pieceAtDestination)) {
                                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(pieceAtDestination) === false || gameState.IsPlayerTurn() || gameState.Abilities.HasPawnsDestroyCapturingPiece === false) {
                                        ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, kingMove.Item1, kingMove.Item2, piece, gameState, kingMove.Item1, kingMove.Item2, moves);
                                    }
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                },
                GetMoveInfosForCastlingAndSuperCastlingMoves: function (startingFile, startingRank, endingFile, endingRank, interimBoard, newBoard, gameState, moves) {
                    if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(gameState.Board, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, startingFile, startingRank)) {
                        return;
                    }

                    if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(interimBoard, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, ((Bridge.Int.div((((startingFile + endingFile) | 0)), 2)) | 0), ((Bridge.Int.div((((startingRank + endingRank) | 0)), 2)) | 0))) {
                        return;
                    }

                    if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(newBoard, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, endingFile, endingRank)) {
                        return;
                    }

                    if (gameState.IsWhiteTurn) {
                        var isPromotion;

                        isPromotion = startingRank === 7 && endingRank === 7 && gameState.IsPlayerTurn() && gameState.Abilities.HasAnyPieceCanPromote;

                        if (isPromotion) {
                            ChessCompStompWithHacksEngine.ComputeMoves.AddPromotionMoveInfos(startingFile, startingRank, endingFile, endingRank, false, moves);
                        } else {
                            moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.NormalMove(startingFile, startingRank, endingFile, endingRank), false));
                        }
                    } else {
                        var isPromotion1;

                        isPromotion1 = startingRank === 0 && endingRank === 0 && gameState.IsPlayerTurn() && gameState.Abilities.HasAnyPieceCanPromote;

                        if (isPromotion1) {
                            ChessCompStompWithHacksEngine.ComputeMoves.AddPromotionMoveInfos(startingFile, startingRank, endingFile, endingRank, false, moves);
                        } else {
                            moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.NormalMove(startingFile, startingRank, endingFile, endingRank), false));
                        }
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesKnight", {
        statics: {
            methods: {
                AddKnightMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    var $t;
                    var piece = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;

                    var knightMoves = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 2) | 0) });
                    knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 2) | 0) });
                    knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 2) | 0) });
                    knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 2) | 0) });
                    knightMoves.add({ Item1: ((i + 2) | 0), Item2: ((j + 1) | 0) });
                    knightMoves.add({ Item1: ((i + 2) | 0), Item2: ((j - 1) | 0) });
                    knightMoves.add({ Item1: ((i - 2) | 0), Item2: ((j + 1) | 0) });
                    knightMoves.add({ Item1: ((i - 2) | 0), Item2: ((j - 1) | 0) });
                    if (gameState.IsPlayerTurn() && gameState.Abilities.CanKnightsMakeLargeKnightsMove) {
                        knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 3) | 0) });
                        knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 3) | 0) });
                        knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 3) | 0) });
                        knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 3) | 0) });
                        knightMoves.add({ Item1: ((i + 3) | 0), Item2: ((j + 1) | 0) });
                        knightMoves.add({ Item1: ((i + 3) | 0), Item2: ((j - 1) | 0) });
                        knightMoves.add({ Item1: ((i - 3) | 0), Item2: ((j + 1) | 0) });
                        knightMoves.add({ Item1: ((i - 3) | 0), Item2: ((j - 1) | 0) });
                    }

                    $t = Bridge.getEnumerator(knightMoves);
                    try {
                        while ($t.moveNext()) {
                            var knightMove = $t.Current;
                            if (0 <= knightMove.Item1 && knightMove.Item1 < 8 && 0 <= knightMove.Item2 && knightMove.Item2 < 8) {
                                var pieceAtDestination = gameState.Board.GetPiece$1(knightMove.Item1, knightMove.Item2);

                                if (gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(pieceAtDestination) || !gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(pieceAtDestination)) {
                                    ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, knightMove.Item1, knightMove.Item2, piece, gameState, kingFile, kingRank, moves);
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesPawn", {
        statics: {
            methods: {
                AddPawnMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    ChessCompStompWithHacksEngine.ComputeMovesPawn.AddNonCapturePawnMoves(gameState, i, j, kingFile, kingRank, moves);

                    ChessCompStompWithHacksEngine.ComputeMovesPawn.AddCapturingPawnMoves(gameState, i, j, kingFile, kingRank, moves);
                },
                AddNonCapturePawnMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    var nextRank;
                    var nextNextRank;
                    var nextNextNextRank;
                    var piece = new ChessCompStompWithHacksEngine.ChessSquarePiece();

                    if (gameState.IsWhiteTurn) {
                        nextRank = (j + 1) | 0;
                        nextNextRank = (j + 2) | 0;
                        nextNextNextRank = (j + 3) | 0;
                        piece = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                    } else {
                        nextRank = (j - 1) | 0;
                        nextNextRank = (j - 2) | 0;
                        nextNextNextRank = (j - 3) | 0;
                        piece = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                    }

                    if (gameState.Board.GetPiece$1(i, nextRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                        ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, i, nextRank, piece, gameState, kingFile, kingRank, moves);

                        if (gameState.UnmovedPawns.HasUnmovedPawn(i, j) && nextNextRank >= 0 && nextNextRank < 8 && gameState.Board.GetPiece$1(i, nextNextRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                            ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, i, nextNextRank, piece, gameState, kingFile, kingRank, moves);

                            if (gameState.IsPlayerTurn() && gameState.Abilities.CanPawnsMoveThreeSpacesInitially && nextNextNextRank >= 0 && nextNextNextRank < 8 && gameState.Board.GetPiece$1(i, nextNextNextRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, i, nextNextNextRank, piece, gameState, kingFile, kingRank, moves);
                            }
                        }
                    }
                },
                AddCapturingPawnMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    var $t;
                    var hasSuperEnPassant = gameState.IsPlayerTurn() && gameState.Abilities.CanSuperEnPassant;

                    var nextRank = gameState.IsWhiteTurn ? (((j + 1) | 0)) : (((j - 1) | 0));
                    var isOnCorrectRankForNormalEnPassant = gameState.IsWhiteTurn ? (j === 4) : (j === 3);

                    var endingFiles = new (System.Collections.Generic.List$1(System.Int32)).ctor();
                    if (i >= 1) {
                        endingFiles.add(((i - 1) | 0));
                    }
                    if (i <= 6) {
                        endingFiles.add(((i + 1) | 0));
                    }

                    $t = Bridge.getEnumerator(endingFiles);
                    try {
                        while ($t.moveNext()) {
                            var endingFile = $t.Current;
                            var capturingSquare = gameState.Board.GetPiece$1(endingFile, nextRank);
                            var enPassantSquare = gameState.Board.GetPiece$1(endingFile, j);

                            if (gameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(capturingSquare) || !gameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(capturingSquare)) {
                                continue;
                            }

                            var shouldTakeEnPassant;
                            var hasCapturedOpponentPawn;

                            if (hasSuperEnPassant) {
                                if (!ChessCompStompWithHacksEngine.ComputeMovesPawn.IsOpponentPiece(gameState, capturingSquare) && !ChessCompStompWithHacksEngine.ComputeMovesPawn.IsOpponentPiece(gameState, enPassantSquare)) {
                                    continue;
                                }
                                shouldTakeEnPassant = ChessCompStompWithHacksEngine.ComputeMovesPawn.IsOpponentPiece(gameState, enPassantSquare);
                                hasCapturedOpponentPawn = shouldTakeEnPassant && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(enPassantSquare) || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(capturingSquare);
                            } else {
                                if (System.Nullable.hasValue(gameState.PreviousPawnMoveFileForEnPassant) && System.Nullable.getValue(gameState.PreviousPawnMoveFileForEnPassant) === endingFile && isOnCorrectRankForNormalEnPassant && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(enPassantSquare) && capturingSquare === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    shouldTakeEnPassant = true;
                                    hasCapturedOpponentPawn = true;
                                } else {
                                    if (!ChessCompStompWithHacksEngine.ComputeMovesPawn.IsOpponentPiece(gameState, capturingSquare)) {
                                        continue;
                                    }
                                    shouldTakeEnPassant = false;
                                    hasCapturedOpponentPawn = ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(capturingSquare);
                                }
                            }

                            var newBoard = gameState.Board.SetPiece(i, j, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);

                            if (shouldTakeEnPassant) {
                                newBoard = newBoard.SetPiece(endingFile, j, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                            }

                            if (!gameState.IsPlayerTurn() && gameState.Abilities.HasPawnsDestroyCapturingPiece && hasCapturedOpponentPawn) {
                                newBoard = newBoard.SetPiece(endingFile, nextRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                            } else {
                                newBoard = newBoard.SetPiece(endingFile, nextRank, gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook);
                            }

                            ChessCompStompWithHacksEngine.ComputeMovesPawn.AddMoveInfosForCapturingPawnMove(i, j, endingFile, nextRank, newBoard, gameState, kingFile, kingRank, moves);
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                },
                IsOpponentPiece: function (gameState, piece) {
                    return gameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(piece) || !gameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece);
                },
                AddMoveInfosForCapturingPawnMove: function (startingFile, startingRank, endingFile, endingRank, newBoard, gameState, kingFile, kingRank, moves) {
                    if (ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(newBoard, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, kingFile, kingRank)) {
                        return;
                    }

                    var isPromotion = gameState.IsWhiteTurn ? (endingRank === 7) : (endingRank === 0);

                    if (isPromotion) {
                        ChessCompStompWithHacksEngine.ComputeMoves.AddPromotionMoveInfos(startingFile, startingRank, endingFile, endingRank, true, moves);
                    } else {
                        moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.NormalMove(startingFile, startingRank, endingFile, endingRank), true));
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesQueen", {
        statics: {
            methods: {
                AddQueenMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    var $t, $t1;
                    var piece = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;

                    var deltas = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    deltas.add({ Item1: 0, Item2: 1 });
                    deltas.add({ Item1: 0, Item2: -1 });
                    deltas.add({ Item1: 1, Item2: 0 });
                    deltas.add({ Item1: -1, Item2: 0 });
                    deltas.add({ Item1: 1, Item2: 1 });
                    deltas.add({ Item1: 1, Item2: -1 });
                    deltas.add({ Item1: -1, Item2: 1 });
                    deltas.add({ Item1: -1, Item2: -1 });

                    $t = Bridge.getEnumerator(deltas);
                    try {
                        while ($t.moveNext()) {
                            var delta = $t.Current;
                            var endI = i;
                            var endJ = j;
                            while (true) {
                                endI = (endI + delta.Item1) | 0;
                                endJ = (endJ + delta.Item2) | 0;
                                if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8) {
                                    break;
                                }

                                var pieceAtDestination = gameState.Board.GetPiece$1(endI, endJ);

                                if (gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(pieceAtDestination) || !gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(pieceAtDestination)) {
                                    ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, endI, endJ, piece, gameState, kingFile, kingRank, moves);
                                }

                                if (pieceAtDestination !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    break;
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    if (gameState.IsPlayerTurn() && gameState.Abilities.CanQueensMoveLikeKnights) {
                        var knightMoves = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                        knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 2) | 0) });
                        knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 2) | 0) });
                        knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 2) | 0) });
                        knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 2) | 0) });
                        knightMoves.add({ Item1: ((i + 2) | 0), Item2: ((j + 1) | 0) });
                        knightMoves.add({ Item1: ((i + 2) | 0), Item2: ((j - 1) | 0) });
                        knightMoves.add({ Item1: ((i - 2) | 0), Item2: ((j + 1) | 0) });
                        knightMoves.add({ Item1: ((i - 2) | 0), Item2: ((j - 1) | 0) });
                        if (gameState.IsPlayerTurn() && gameState.Abilities.CanKnightsMakeLargeKnightsMove) {
                            knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 3) | 0) });
                            knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 3) | 0) });
                            knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 3) | 0) });
                            knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 3) | 0) });
                            knightMoves.add({ Item1: ((i + 3) | 0), Item2: ((j + 1) | 0) });
                            knightMoves.add({ Item1: ((i + 3) | 0), Item2: ((j - 1) | 0) });
                            knightMoves.add({ Item1: ((i - 3) | 0), Item2: ((j + 1) | 0) });
                            knightMoves.add({ Item1: ((i - 3) | 0), Item2: ((j - 1) | 0) });
                        }

                        $t1 = Bridge.getEnumerator(knightMoves);
                        try {
                            while ($t1.moveNext()) {
                                var knightMove = $t1.Current;
                                if (0 <= knightMove.Item1 && knightMove.Item1 < 8 && 0 <= knightMove.Item2 && knightMove.Item2 < 8) {
                                    var pieceAtDestination1 = gameState.Board.GetPiece$1(knightMove.Item1, knightMove.Item2);

                                    if (gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(pieceAtDestination1) || !gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(pieceAtDestination1)) {
                                        ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, knightMove.Item1, knightMove.Item2, piece, gameState, kingFile, kingRank, moves);
                                    }
                                }
                            }
                        } finally {
                            if (Bridge.is($t1, System.IDisposable)) {
                                $t1.System$IDisposable$Dispose();
                            }
                        }
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesRook", {
        statics: {
            methods: {
                AddRookMoves: function (gameState, i, j, kingFile, kingRank, moves) {
                    var $t;
                    var piece = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;

                    var deltas = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                    deltas.add({ Item1: 0, Item2: 1 });
                    deltas.add({ Item1: 0, Item2: -1 });
                    deltas.add({ Item1: 1, Item2: 0 });
                    deltas.add({ Item1: -1, Item2: 0 });
                    if (gameState.IsPlayerTurn() && gameState.Abilities.CanRooksMoveLikeBishops) {
                        deltas.add({ Item1: 1, Item2: 1 });
                        deltas.add({ Item1: 1, Item2: -1 });
                        deltas.add({ Item1: -1, Item2: 1 });
                        deltas.add({ Item1: -1, Item2: -1 });
                    }

                    $t = Bridge.getEnumerator(deltas);
                    try {
                        while ($t.moveNext()) {
                            var delta = $t.Current;
                            var endI = i;
                            var endJ = j;
                            while (true) {
                                endI = (endI + delta.Item1) | 0;
                                endJ = (endJ + delta.Item2) | 0;
                                if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8) {
                                    break;
                                }

                                var pieceAtDestination = gameState.Board.GetPiece$1(endI, endJ);

                                if (gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(pieceAtDestination) || !gameState.IsWhiteTurn && !ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(pieceAtDestination)) {
                                    ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, endI, endJ, piece, gameState, kingFile, kingRank, moves);
                                }

                                if (pieceAtDestination !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    if (gameState.IsPlayerTurn() && gameState.Abilities.CanRooksCaptureLikeCannons) {
                                        while (true) {
                                            endI = (endI + delta.Item1) | 0;
                                            endJ = (endJ + delta.Item2) | 0;
                                            if (endI < 0 || endI >= 8 || endJ < 0 || endJ >= 8) {
                                                break;
                                            }

                                            if (gameState.Board.GetPiece$1(endI, endJ) !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                                if (gameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(gameState.Board.GetPiece$1(endI, endJ)) || !gameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(gameState.Board.GetPiece$1(endI, endJ))) {
                                                    ChessCompStompWithHacksEngine.ComputeMoves.AddMoveInfosForNonEnPassantNonCastlingMoves(i, j, endI, endJ, piece, gameState, kingFile, kingRank, moves);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ComputeMovesTacticalNuke", {
        statics: {
            methods: {
                AddTacticalNukeMoves: function (gameState, kingFile, kingRank, moves) {
                    var $t;
                    if (!gameState.IsPlayerTurn()) {
                        return;
                    }

                    if (!gameState.Abilities.HasTacticalNuke) {
                        return;
                    }

                    if (gameState.HasUsedNuke) {
                        return;
                    }

                    if (gameState.TurnCount <= ChessCompStompWithHacksEngine.TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable) {
                        return;
                    }

                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            var nukedSquares = ChessCompStompWithHacksEngine.TacticalNukeUtil.GetNukedSquares$1(i, j);

                            var eitherKingGetsNuked = false;
                            var nukedBoard = gameState.Board;

                            $t = Bridge.getEnumerator(nukedSquares);
                            try {
                                while ($t.moveNext()) {
                                    var nukedSquare = $t.Current;
                                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKing(gameState.Board.GetPiece$1(nukedSquare.File, nukedSquare.Rank))) {
                                        eitherKingGetsNuked = true;
                                        break;
                                    }

                                    nukedBoard = nukedBoard.SetPiece(nukedSquare.File, nukedSquare.Rank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                                }
                            } finally {
                                if (Bridge.is($t, System.IDisposable)) {
                                    $t.System$IDisposable$Dispose();
                                }
                            }

                            if (eitherKingGetsNuked) {
                                continue;
                            }

                            if (!ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(nukedBoard, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, kingFile, kingRank)) {
                                moves.add(new ChessCompStompWithHacksEngine.ComputeMoves.MoveInfo(ChessCompStompWithHacksEngine.Move.TacticalNukeMove(i, j), false));
                            }
                        }
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.DisplayMove", {
        statics: {
            methods: {
                GetDisplayMoves: function (moves, gameState) {
                    var $t;
                    var displayMoves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.DisplayMove)).ctor();

                    $t = Bridge.getEnumerator(moves);
                    try {
                        while ($t.moveNext()) {
                            var move = $t.Current;
                            displayMoves.add(new ChessCompStompWithHacksEngine.DisplayMove.ctor(move));

                            if (ChessCompStompWithHacksEngine.MoveUtil.IsCastlingOrSuperCastling(move, gameState.Board)) {
                                var king = new ChessCompStompWithHacksEngine.ChessSquare(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank));

                                var direction;
                                if (((move.EndingFile - System.Nullable.getValue(move.StartingFile)) | 0) === 2) {
                                    direction = { Item1: 1, Item2: 0 };
                                } else {
                                    if (((move.EndingFile - System.Nullable.getValue(move.StartingFile)) | 0) === -2) {
                                        direction = { Item1: -1, Item2: 0 };
                                    } else {
                                        if (((move.EndingRank - System.Nullable.getValue(move.StartingRank)) | 0) === 2) {
                                            direction = { Item1: 0, Item2: 1 };
                                        } else {
                                            if (((move.EndingRank - System.Nullable.getValue(move.StartingRank)) | 0) === -2) {
                                                direction = { Item1: 0, Item2: -1 };
                                            } else {
                                                throw new System.Exception();
                                            }
                                        }
                                    }
                                }

                                var rook = king;
                                while (true) {
                                    rook = new ChessCompStompWithHacksEngine.ChessSquare(((rook.File + direction.Item1) | 0), ((rook.Rank + direction.Item2) | 0));
                                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsRook(gameState.Board.GetPiece(rook))) {
                                        break;
                                    }
                                }

                                if (rook.File !== move.EndingFile || rook.Rank !== move.EndingRank) {
                                    displayMoves.add(new ChessCompStompWithHacksEngine.DisplayMove.$ctor1(false, System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank), rook.File, rook.Rank, move.Promotion, move));
                                }

                                displayMoves.add(new ChessCompStompWithHacksEngine.DisplayMove.$ctor1(false, rook.File, rook.Rank, System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank), move.Promotion, move));
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    return displayMoves;
                }
            }
        },
        fields: {
            IsNuke: false,
            StartingFile: null,
            StartingRank: null,
            EndingFile: 0,
            EndingRank: 0,
            Promotion: null,
            Move: null
        },
        ctors: {
            $ctor1: function (isNuke, startingFile, startingRank, endingFile, endingRank, promotion, move) {
                this.$initialize();
                this.IsNuke = isNuke;
                this.StartingFile = startingFile;
                this.StartingRank = startingRank;
                this.EndingFile = endingFile;
                this.EndingRank = endingRank;
                this.Promotion = promotion;
                this.Move = move;
            },
            ctor: function (move) {
                this.$initialize();
                this.IsNuke = move.IsNuke;
                this.StartingFile = move.StartingFile;
                this.StartingRank = move.StartingRank;
                this.EndingFile = move.EndingFile;
                this.EndingRank = move.EndingRank;
                this.Promotion = move.Promotion;
                this.Move = move;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.GameState", {
        fields: {
            Board: null,
            UnmovedPawns: null,
            TurnCount: 0,
            HasUsedNuke: false,
            IsPlayerWhite: false,
            IsWhiteTurn: false,
            PreviousPawnMoveFileForEnPassant: null,
            Castling: null,
            Abilities: null
        },
        ctors: {
            ctor: function (board, unmovedPawns, turnCount, hasUsedNuke, isPlayerWhite, isWhiteTurn, previousPawnMoveFileForEnPassant, castlingRights, playerAbilities) {
                this.$initialize();
                this.Board = board;
                this.UnmovedPawns = unmovedPawns;
                this.TurnCount = turnCount;
                this.HasUsedNuke = hasUsedNuke;
                this.IsPlayerWhite = isPlayerWhite;
                this.IsWhiteTurn = isWhiteTurn;
                this.PreviousPawnMoveFileForEnPassant = previousPawnMoveFileForEnPassant;
                this.Castling = castlingRights;
                this.Abilities = playerAbilities;
            }
        },
        methods: {
            IsPlayerTurn: function () {
                if (this.IsPlayerWhite && this.IsWhiteTurn) {
                    return true;
                }
                if (!this.IsPlayerWhite && !this.IsWhiteTurn) {
                    return true;
                }
                return false;
            }
        }
    });

    /** @namespace ChessCompStompWithHacksEngine */

    /**
     * Tracks whether castling is still allowed.
     Note that this doesn't affect super-castling, which allows the player to castle
     regardless of whether the king or rook has ever moved.
     *
     * @public
     * @class ChessCompStompWithHacksEngine.GameState.CastlingRights
     */
    Bridge.define("ChessCompStompWithHacksEngine.GameState.CastlingRights", {
        $kind: "nested class",
        fields: {
            CanWhiteCastleKingside: false,
            CanWhiteCastleQueenside: false,
            CanBlackCastleKingside: false,
            CanBlackCastleQueenside: false
        },
        ctors: {
            ctor: function (canWhiteCastleKingside, canWhiteCastleQueenside, canBlackCastleKingside, canBlackCastleQueenside) {
                this.$initialize();
                this.CanWhiteCastleKingside = canWhiteCastleKingside;
                this.CanWhiteCastleQueenside = canWhiteCastleQueenside;
                this.CanBlackCastleKingside = canBlackCastleKingside;
                this.CanBlackCastleQueenside = canBlackCastleQueenside;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.GameState.PlayerAbilities", {
        $kind: "nested class",
        fields: {
            CanPawnsMoveThreeSpacesInitially: false,
            CanSuperEnPassant: false,
            CanRooksMoveLikeBishops: false,
            CanSuperCastle: false,
            CanRooksCaptureLikeCannons: false,
            CanKnightsMakeLargeKnightsMove: false,
            CanQueensMoveLikeKnights: false,
            HasTacticalNuke: false,
            HasAnyPieceCanPromote: false,
            HasStalemateIsVictory: false,
            HasOpponentMustCaptureWhenPossible: false,
            HasPawnsDestroyCapturingPiece: false
        },
        ctors: {
            ctor: function (canPawnsMoveThreeSpacesInitially, canSuperEnPassant, canRooksMoveLikeBishops, canSuperCastle, canRooksCaptureLikeCannons, canKnightsMakeLargeKnightsMove, canQueensMoveLikeKnights, hasTacticalNuke, hasAnyPieceCanPromote, hasStalemateIsVictory, hasOpponentMustCaptureWhenPossible, hasPawnsDestroyCapturingPiece) {
                this.$initialize();
                this.CanPawnsMoveThreeSpacesInitially = canPawnsMoveThreeSpacesInitially;
                this.CanSuperEnPassant = canSuperEnPassant;
                this.CanRooksMoveLikeBishops = canRooksMoveLikeBishops;
                this.CanSuperCastle = canSuperCastle;
                this.CanRooksCaptureLikeCannons = canRooksCaptureLikeCannons;
                this.CanKnightsMakeLargeKnightsMove = canKnightsMakeLargeKnightsMove;
                this.CanQueensMoveLikeKnights = canQueensMoveLikeKnights;

                this.HasTacticalNuke = hasTacticalNuke;
                this.HasAnyPieceCanPromote = hasAnyPieceCanPromote;
                this.HasStalemateIsVictory = hasStalemateIsVictory;
                this.HasOpponentMustCaptureWhenPossible = hasOpponentMustCaptureWhenPossible;
                this.HasPawnsDestroyCapturingPiece = hasPawnsDestroyCapturingPiece;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.GameStateUtil", {
        statics: {
            methods: {
                GetGameStateWithoutNukeAbility: function (gameState) {
                    if (gameState.Abilities.HasTacticalNuke === false) {
                        return gameState;
                    }

                    return new ChessCompStompWithHacksEngine.GameState(gameState.Board, gameState.UnmovedPawns, gameState.TurnCount, false, gameState.IsPlayerWhite, gameState.IsWhiteTurn, gameState.PreviousPawnMoveFileForEnPassant, gameState.Castling, new ChessCompStompWithHacksEngine.GameState.PlayerAbilities(gameState.Abilities.CanPawnsMoveThreeSpacesInitially, gameState.Abilities.CanSuperEnPassant, gameState.Abilities.CanRooksMoveLikeBishops, gameState.Abilities.CanSuperCastle, gameState.Abilities.CanRooksCaptureLikeCannons, gameState.Abilities.CanKnightsMakeLargeKnightsMove, gameState.Abilities.CanQueensMoveLikeKnights, false, gameState.Abilities.HasAnyPieceCanPromote, gameState.Abilities.HasStalemateIsVictory, gameState.Abilities.HasOpponentMustCaptureWhenPossible, gameState.Abilities.HasPawnsDestroyCapturingPiece));
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.Hack", {
        $kind: "enum",
        statics: {
            fields: {
                ExtraPawnFirst: 0,
                ExtraPawnSecond: 1,
                ExtraQueen: 2,
                PawnsCanMoveThreeSpacesInitially: 3,
                SuperEnPassant: 4,
                RooksCanMoveLikeBishops: 5,
                SuperCastling: 6,
                RooksCanCaptureLikeCannons: 7,
                KnightsCanMakeLargeKnightsMove: 8,
                QueensCanMoveLikeKnights: 9,
                TacticalNuke: 10,
                AnyPieceCanPromote: 11,
                StalemateIsVictory: 12,
                OpponentMustCaptureWhenPossible: 13,
                PawnsDestroyCapturingPiece: 14
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.IBoardEvaluator", {
        $kind: "interface"
    });

    Bridge.define("ChessCompStompWithHacksEngine.Move", {
        statics: {
            methods: {
                NormalMove: function (startingFile, startingRank, endingFile, endingRank) {
                    return new ChessCompStompWithHacksEngine.Move(false, startingFile, startingRank, endingFile, endingRank, null);
                },
                PromotionMove: function (startingFile, startingRank, endingFile, endingRank, promotion) {
                    return new ChessCompStompWithHacksEngine.Move(false, startingFile, startingRank, endingFile, endingRank, promotion);
                },
                TacticalNukeMove: function (file, rank) {
                    return new ChessCompStompWithHacksEngine.Move(true, null, null, file, rank, null);
                }
            }
        },
        fields: {
            IsNuke: false,
            StartingFile: null,
            StartingRank: null,
            EndingFile: 0,
            EndingRank: 0,
            Promotion: null
        },
        ctors: {
            ctor: function (isNuke, startingFile, startingRank, endingFile, endingRank, promotion) {
                this.$initialize();
                this.IsNuke = isNuke;
                this.StartingFile = startingFile;
                this.StartingRank = startingRank;
                this.EndingFile = endingFile;
                this.EndingRank = endingRank;
                this.Promotion = promotion;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.Move.PromotionType", {
        $kind: "nested enum",
        statics: {
            fields: {
                PromoteToRook: 0,
                PromoteToKnight: 1,
                PromoteToBishop: 2,
                PromoteToQueen: 3
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.MoveImplementation", {
        statics: {
            methods: {
                ApplyMove: function (gameState, displayMove) {
                    return ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, displayMove.Move);
                },
                ApplyMove$1: function (gameState, move) {
                    var newBoard = ChessCompStompWithHacksEngine.MoveImplementation.GetNewBoard(gameState, move);

                    var unmovedPawns = gameState.UnmovedPawns;
                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            if (unmovedPawns.HasUnmovedPawn(i, j)) {
                                if (newBoard.GetPiece$1(i, j) !== gameState.Board.GetPiece$1(i, j)) {
                                    unmovedPawns = unmovedPawns.PawnMoved(i, j);
                                }
                            }
                        }
                    }

                    var previousPawnMoveFileForEnPassant;
                    if (move.IsNuke) {
                        previousPawnMoveFileForEnPassant = null;
                    } else {
                        var pieceThatMoved = gameState.Board.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank));
                        var pieceWasPawnAndMovedTwoSpaces = ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(pieceThatMoved) && System.Nullable.getValue(move.StartingFile) === move.EndingFile && Math.abs(((System.Nullable.getValue(move.StartingRank) - move.EndingRank) | 0)) === 2;
                        var pieceWasPawnAndMovedTwoSpacesFromSecondRank = pieceWasPawnAndMovedTwoSpaces && (System.Nullable.getValue(move.StartingRank) === 1 || System.Nullable.getValue(move.StartingRank) === 6);

                        if (pieceWasPawnAndMovedTwoSpacesFromSecondRank) {
                            previousPawnMoveFileForEnPassant = System.Nullable.getValue(move.StartingFile);
                        } else {
                            previousPawnMoveFileForEnPassant = null;
                        }
                    }

                    return new ChessCompStompWithHacksEngine.GameState(newBoard, unmovedPawns, ((gameState.TurnCount + 1) | 0), move.IsNuke ? true : gameState.HasUsedNuke, gameState.IsPlayerWhite, !gameState.IsWhiteTurn, previousPawnMoveFileForEnPassant, ChessCompStompWithHacksEngine.MoveImplementation.ComputeNewCastlingRights(gameState, newBoard), gameState.Abilities);
                },
                GetNewBoard: function (gameState, move) {
                    var $t;
                    if (move.IsNuke) {
                        var nukedSquares = ChessCompStompWithHacksEngine.TacticalNukeUtil.GetNukedSquares$1(move.EndingFile, move.EndingRank);

                        var newBoard = gameState.Board;
                        $t = Bridge.getEnumerator(nukedSquares);
                        try {
                            while ($t.moveNext()) {
                                var nukedSquare = $t.Current;
                                newBoard = newBoard.SetPiece(nukedSquare.File, nukedSquare.Rank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                            }
                        } finally {
                            if (Bridge.is($t, System.IDisposable)) {
                                $t.System$IDisposable$Dispose();
                            }
                        }

                        return newBoard;
                    } else {
                        var pieceThatMoved = gameState.Board.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank));

                        if (ChessCompStompWithHacksEngine.MoveUtil.IsCastlingOrSuperCastling(move, gameState.Board)) {
                            return ChessCompStompWithHacksEngine.MoveImplementation.GetNewBoard_CastlingOrSuperCastling(gameState, move);
                        }

                        var movedPawnAndCapturedSomething = ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(pieceThatMoved) && System.Nullable.getValue(move.StartingFile) !== move.EndingFile;

                        var wasEnPassantOrSuperEnPassant;

                        if (!movedPawnAndCapturedSomething) {
                            wasEnPassantOrSuperEnPassant = false;
                        } else {
                            if (gameState.Board.GetPiece$1(move.EndingFile, move.EndingRank) === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                wasEnPassantOrSuperEnPassant = true;
                            } else {
                                if (gameState.IsPlayerTurn() === false) {
                                    wasEnPassantOrSuperEnPassant = false;
                                } else {
                                    if (gameState.Abilities.CanSuperEnPassant === false) {
                                        wasEnPassantOrSuperEnPassant = false;
                                    } else {
                                        var potentialEnPassantCapturedPiece = gameState.Board.GetPiece$1(move.EndingFile, System.Nullable.getValue(move.StartingRank));
                                        if (potentialEnPassantCapturedPiece === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                            wasEnPassantOrSuperEnPassant = false;
                                        } else {
                                            wasEnPassantOrSuperEnPassant = ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(potentialEnPassantCapturedPiece) && gameState.IsWhiteTurn === false || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(potentialEnPassantCapturedPiece) && gameState.IsWhiteTurn;
                                        }
                                    }
                                }
                            }
                        }

                        var newBoard1 = gameState.Board.SetPiece(move.EndingFile, move.EndingRank, gameState.Board.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank))).SetPiece(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank), ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);

                        if (System.Nullable.hasValue(move.Promotion)) {
                            newBoard1 = newBoard1.SetPiece(move.EndingFile, move.EndingRank, ChessCompStompWithHacksEngine.PromotionTypeUtil.GetPromotedPiece(System.Nullable.getValue(move.Promotion), gameState.IsWhiteTurn));
                        }

                        if (gameState.IsPlayerTurn() === false && gameState.Abilities.HasPawnsDestroyCapturingPiece) {
                            if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(gameState.Board.GetPiece$1(move.EndingFile, move.EndingRank))) {
                                newBoard1 = newBoard1.SetPiece(move.EndingFile, move.EndingRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                            } else {
                                if (wasEnPassantOrSuperEnPassant) {
                                    newBoard1 = newBoard1.SetPiece(move.EndingFile, move.EndingRank, ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                                }
                            }
                        }

                        if (wasEnPassantOrSuperEnPassant) {
                            newBoard1 = newBoard1.SetPiece(move.EndingFile, System.Nullable.getValue(move.StartingRank), ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                        }

                        return newBoard1;
                    }
                },
                GetNewBoard_CastlingOrSuperCastling: function (gameState, move) {
                    var delta;

                    if (move.EndingFile > System.Nullable.getValue(move.StartingFile)) {
                        delta = { Item1: 1, Item2: 0 };
                    } else {
                        if (move.EndingFile < System.Nullable.getValue(move.StartingFile)) {
                            delta = { Item1: -1, Item2: 0 };
                        } else {
                            if (move.EndingRank > System.Nullable.getValue(move.StartingRank)) {
                                delta = { Item1: 0, Item2: 1 };
                            } else {
                                if (move.EndingRank < System.Nullable.getValue(move.StartingRank)) {
                                    delta = { Item1: 0, Item2: -1 };
                                } else {
                                    throw new System.Exception();
                                }
                            }
                        }
                    }

                    var rookThatMoved = gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                    var rookThatMovedPossiblyPromoted = System.Nullable.hasValue(move.Promotion) ? ChessCompStompWithHacksEngine.PromotionTypeUtil.GetPromotedPiece(System.Nullable.getValue(move.Promotion), gameState.IsWhiteTurn) : rookThatMoved;

                    var count = 1;
                    while (true) {
                        if (gameState.Board.GetPiece$1(((System.Nullable.getValue(move.StartingFile) + Bridge.Int.mul(delta.Item1, count)) | 0), ((System.Nullable.getValue(move.StartingRank) + Bridge.Int.mul(delta.Item2, count)) | 0)) === rookThatMoved) {
                            break;
                        }
                        count = (count + 1) | 0;
                    }

                    var newBoard = gameState.Board.SetPiece(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank), ChessCompStompWithHacksEngine.ChessSquarePiece.Empty).SetPiece(((System.Nullable.getValue(move.StartingFile) + delta.Item1) | 0), ((System.Nullable.getValue(move.StartingRank) + delta.Item2) | 0), rookThatMovedPossiblyPromoted).SetPiece(((((System.Nullable.getValue(move.StartingFile) + delta.Item1) | 0) + delta.Item1) | 0), ((((System.Nullable.getValue(move.StartingRank) + delta.Item2) | 0) + delta.Item2) | 0), gameState.IsWhiteTurn ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing);

                    if (count > 2) {
                        newBoard = newBoard.SetPiece(((System.Nullable.getValue(move.StartingFile) + Bridge.Int.mul(delta.Item1, count)) | 0), ((System.Nullable.getValue(move.StartingRank) + Bridge.Int.mul(delta.Item2, count)) | 0), ChessCompStompWithHacksEngine.ChessSquarePiece.Empty);
                    }

                    return newBoard;
                },
                ComputeNewCastlingRights: function (gameState, newBoard) {
                    var canWhiteCastleKingside;
                    if (gameState.Castling.CanWhiteCastleKingside) {
                        canWhiteCastleKingside = newBoard.GetPiece$1(4, 0) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing && newBoard.GetPiece$1(7, 0) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                    } else {
                        canWhiteCastleKingside = false;
                    }

                    var canWhiteCastleQueenside;
                    if (gameState.Castling.CanWhiteCastleQueenside) {
                        canWhiteCastleQueenside = newBoard.GetPiece$1(4, 0) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing && newBoard.GetPiece$1(0, 0) === ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                    } else {
                        canWhiteCastleQueenside = false;
                    }

                    var canBlackCastleKingside;
                    if (gameState.Castling.CanBlackCastleKingside) {
                        canBlackCastleKingside = newBoard.GetPiece$1(4, 7) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing && newBoard.GetPiece$1(7, 7) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                    } else {
                        canBlackCastleKingside = false;
                    }

                    var canBlackCastleQueenside;
                    if (gameState.Castling.CanBlackCastleQueenside) {
                        canBlackCastleQueenside = newBoard.GetPiece$1(4, 7) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing && newBoard.GetPiece$1(0, 7) === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                    } else {
                        canBlackCastleQueenside = false;
                    }

                    return new ChessCompStompWithHacksEngine.GameState.CastlingRights(canWhiteCastleKingside, canWhiteCastleQueenside, canBlackCastleKingside, canBlackCastleQueenside);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.MoveNaming", {
        statics: {
            methods: {
                GetNameOfMove: function (move, originalGameState) {
                    if (move.IsNuke) {
                        return ChessCompStompWithHacksEngine.MoveNaming.GetNameOfNukeMove(move, originalGameState);
                    }

                    var startingFile = System.Nullable.getValue(move.StartingFile);
                    var startingRank = System.Nullable.getValue(move.StartingRank);

                    var pieceBeingMoved = originalGameState.Board.GetPiece$1(startingFile, startingRank);

                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(pieceBeingMoved)) {
                        return ChessCompStompWithHacksEngine.MoveNaming.GetNameOfPawnMove(move, originalGameState);
                    }

                    if (ChessCompStompWithHacksEngine.MoveUtil.IsCastlingOrSuperCastling(move, originalGameState.Board)) {
                        return ChessCompStompWithHacksEngine.MoveNaming.GetNameOfCastlingMove(move, originalGameState);
                    }

                    return ChessCompStompWithHacksEngine.MoveNaming.GetNameOfNonPawnNonCastlingNonNukeMove(move, originalGameState);
                },
                GetNameOfNonPawnNonCastlingNonNukeMove: function (move, originalGameState) {
                    var $t, $t1, $t2;
                    var name;

                    var pieceBeingMoved = originalGameState.Board.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank));

                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsRook(pieceBeingMoved)) {
                        name = "R";
                    } else {
                        if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKnight(pieceBeingMoved)) {
                            name = "N";
                        } else {
                            if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBishop(pieceBeingMoved)) {
                                name = "B";
                            } else {
                                if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsQueen(pieceBeingMoved)) {
                                    name = "Q";
                                } else {
                                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKing(pieceBeingMoved)) {
                                        name = "K";
                                    } else {
                                        throw new System.Exception();
                                    }
                                }
                            }
                        }
                    }

                    var allMoves = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(originalGameState).Moves;
                    var piecesThatCanMakeThisMove = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ChessSquare)).ctor();
                    $t = Bridge.getEnumerator(allMoves);
                    try {
                        while ($t.moveNext()) {
                            var m = $t.Current;
                            if (m.IsNuke) {
                                continue;
                            }

                            if (originalGameState.Board.GetPiece$1(System.Nullable.getValue(m.StartingFile), System.Nullable.getValue(m.StartingRank)) !== pieceBeingMoved) {
                                continue;
                            }

                            if (m.EndingFile !== move.EndingFile || m.EndingRank !== move.EndingRank) {
                                continue;
                            }

                            if (System.Nullable.hasValue(m.Promotion) && System.Nullable.getValue(m.Promotion) !== ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen) {
                                continue;
                            }

                            piecesThatCanMakeThisMove.add(new ChessCompStompWithHacksEngine.ChessSquare(System.Nullable.getValue(m.StartingFile), System.Nullable.getValue(m.StartingRank)));
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    if (piecesThatCanMakeThisMove.Count > 1) {
                        var fileCount = 0;
                        $t1 = Bridge.getEnumerator(piecesThatCanMakeThisMove);
                        try {
                            while ($t1.moveNext()) {
                                var cs = $t1.Current;
                                if (cs.File === System.Nullable.getValue(move.StartingFile)) {
                                    fileCount = (fileCount + 1) | 0;
                                }
                            }
                        } finally {
                            if (Bridge.is($t1, System.IDisposable)) {
                                $t1.System$IDisposable$Dispose();
                            }
                        }

                        if (fileCount === 1) {
                            name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(System.Nullable.getValue(move.StartingFile)) || "");
                        } else {
                            var rankCount = 0;
                            $t2 = Bridge.getEnumerator(piecesThatCanMakeThisMove);
                            try {
                                while ($t2.moveNext()) {
                                    var cs1 = $t2.Current;
                                    if (cs1.Rank === System.Nullable.getValue(move.StartingRank)) {
                                        rankCount = (rankCount + 1) | 0;
                                    }
                                }
                            } finally {
                                if (Bridge.is($t2, System.IDisposable)) {
                                    $t2.System$IDisposable$Dispose();
                                }
                            }

                            if (rankCount === 1) {
                                name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(System.Nullable.getValue(move.StartingRank)) || "");
                            } else {
                                name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(System.Nullable.getValue(move.StartingFile)) || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(System.Nullable.getValue(move.StartingRank)) || "");
                            }
                        }
                    }

                    if (ChessCompStompWithHacksEngine.MoveUtil.IsCapturingMove(move, originalGameState.Board)) {
                        name = (name || "") + "x";
                    }

                    name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(move.EndingFile) || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(move.EndingRank) || "");

                    if (move.Promotion != null) {
                        name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetPromotionString(move) || "");
                    }

                    var moveResult = ChessCompStompWithHacksEngine.MoveNaming.ApplyMove(move, originalGameState);

                    if (moveResult.IsCheck) {
                        name = (name || "") + "+";
                    }
                    if (moveResult.IsCheckmate) {
                        name = (name || "") + "#";
                    }

                    return name;
                },
                /**
                 * Returns the number of squares the rook moves
                 *
                 * @static
                 * @private
                 * @this ChessCompStompWithHacksEngine.MoveNaming
                 * @memberof ChessCompStompWithHacksEngine.MoveNaming
                 * @param   {ChessCompStompWithHacksEngine.Move}         castlingMove         
                 * @param   {ChessCompStompWithHacksEngine.GameState}    originalGameState
                 * @return  {number}
                 */
                GetLengthOfCastlingMove: function (castlingMove, originalGameState) {
                    var direction;
                    if (((castlingMove.EndingFile - System.Nullable.getValue(castlingMove.StartingFile)) | 0) === 2) {
                        direction = { Item1: 1, Item2: 0 };
                    } else {
                        if (((castlingMove.EndingFile - System.Nullable.getValue(castlingMove.StartingFile)) | 0) === -2) {
                            direction = { Item1: -1, Item2: 0 };
                        } else {
                            if (((castlingMove.EndingRank - System.Nullable.getValue(castlingMove.StartingRank)) | 0) === 2) {
                                direction = { Item1: 0, Item2: 1 };
                            } else {
                                if (((castlingMove.EndingRank - System.Nullable.getValue(castlingMove.StartingRank)) | 0) === -2) {
                                    direction = { Item1: 0, Item2: -1 };
                                } else {
                                    throw new System.Exception();
                                }
                            }
                        }
                    }

                    var rook = new ChessCompStompWithHacksEngine.ChessSquare(System.Nullable.getValue(castlingMove.StartingFile), System.Nullable.getValue(castlingMove.StartingRank));
                    var count = 0;
                    while (true) {
                        rook = new ChessCompStompWithHacksEngine.ChessSquare(((rook.File + direction.Item1) | 0), ((rook.Rank + direction.Item2) | 0));
                        count = (count + 1) | 0;
                        if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsRook(originalGameState.Board.GetPiece(rook))) {
                            break;
                        }
                    }

                    return ((count - 1) | 0);
                },
                GetNameOfCastlingMove: function (castlingMove, originalGameState) {
                    var $t;
                    var castlingLength = ChessCompStompWithHacksEngine.MoveNaming.GetLengthOfCastlingMove(castlingMove, originalGameState);

                    var allMoves = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(originalGameState).Moves;

                    var count = 0;
                    $t = Bridge.getEnumerator(allMoves);
                    try {
                        while ($t.moveNext()) {
                            var m = $t.Current;
                            if (ChessCompStompWithHacksEngine.MoveUtil.IsCastlingOrSuperCastling(m, originalGameState.Board)) {
                                if (m.Promotion == null || System.Nullable.getValue(m.Promotion) === ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen) {
                                    var length = ChessCompStompWithHacksEngine.MoveNaming.GetLengthOfCastlingMove(m, originalGameState);
                                    if (length === castlingLength) {
                                        count = (count + 1) | 0;
                                    }
                                }
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }
                    var isAmbiguous = count > 1;

                    var name;

                    if (isAmbiguous || castlingLength === 0) {
                        name = "K" + (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(castlingMove.EndingFile) || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(castlingMove.EndingRank) || "");
                    } else {
                        switch (castlingLength) {
                            case 1: 
                                name = "0";
                                break;
                            case 2: 
                                name = "0-0";
                                break;
                            case 3: 
                                name = "0-0-0";
                                break;
                            case 4: 
                                name = "0-0-0-0";
                                break;
                            case 5: 
                                name = "0-0-0-0-0";
                                break;
                            case 6: 
                                name = "0-0-0-0-0-0";
                                break;
                            default: 
                                throw new System.Exception();
                        }
                    }

                    if (castlingMove.Promotion != null) {
                        name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetPromotionString(castlingMove) || "");
                    }

                    var moveResult = ChessCompStompWithHacksEngine.MoveNaming.ApplyMove(castlingMove, originalGameState);

                    if (moveResult.IsCheck) {
                        name = (name || "") + "+";
                    }
                    if (moveResult.IsCheckmate) {
                        name = (name || "") + "#";
                    }

                    return name;
                },
                GetNameOfPawnMove: function (pawnMove, originalGameState) {
                    var name;
                    var isEnPassantOrSuperEnPassant;

                    var moveResult = ChessCompStompWithHacksEngine.MoveNaming.ApplyMove(pawnMove, originalGameState);

                    if (ChessCompStompWithHacksEngine.MoveUtil.IsCapturingMove(pawnMove, originalGameState.Board)) {
                        name = (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(System.Nullable.getValue(pawnMove.StartingFile)) || "") + "x" + (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(pawnMove.EndingFile) || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(pawnMove.EndingRank) || "");

                        var potentialEnPassantSquare = new ChessCompStompWithHacksEngine.ChessSquare(pawnMove.EndingFile, System.Nullable.getValue(pawnMove.StartingRank));
                        isEnPassantOrSuperEnPassant = originalGameState.Board.GetPiece(potentialEnPassantSquare) !== moveResult.NewGameState.Board.GetPiece(potentialEnPassantSquare);
                    } else {
                        name = (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(pawnMove.EndingFile) || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(pawnMove.EndingRank) || "");
                        isEnPassantOrSuperEnPassant = false;
                    }

                    if (System.Nullable.hasValue(pawnMove.Promotion)) {
                        name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetPromotionString(pawnMove) || "");
                    }

                    if (moveResult.IsCheck) {
                        name = (name || "") + "+";
                    }
                    if (moveResult.IsCheckmate) {
                        name = (name || "") + "#";
                    }

                    if (isEnPassantOrSuperEnPassant) {
                        name = (name || "") + " e.p.";
                    }

                    return name;
                },
                GetPromotionString: function (move) {
                    if (move.Promotion == null) {
                        return "";
                    }

                    switch (System.Nullable.getValue(move.Promotion)) {
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToRook: 
                            return "=R";
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToKnight: 
                            return "=N";
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToBishop: 
                            return "=B";
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen: 
                            return "=Q";
                        default: 
                            throw new System.Exception();
                    }
                },
                GetNameOfNukeMove: function (nukeMove, originalGameState) {
                    var name = "ICBM";
                    name = (name || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetFileName(nukeMove.EndingFile) || "") + (ChessCompStompWithHacksEngine.MoveNaming.GetRankName(nukeMove.EndingRank) || "");

                    var result = ChessCompStompWithHacksEngine.MoveNaming.ApplyMove(nukeMove, originalGameState);

                    if (result.IsCheck) {
                        return (name || "") + "+";
                    }
                    if (result.IsCheckmate) {
                        return (name || "") + "#";
                    }
                    return name;
                },
                ApplyMove: function (move, originalGameState) {
                    var newGameState = ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(originalGameState, move);
                    var result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(newGameState);

                    if (originalGameState.IsWhiteTurn && result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory || !originalGameState.IsWhiteTurn && result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory) {
                        return new ChessCompStompWithHacksEngine.MoveNaming.MoveResult(newGameState, false, true);
                    }

                    var kingFile = null;
                    var kingRank = null;

                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            var piece = newGameState.Board.GetPiece$1(i, j);
                            if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKing(piece)) {
                                if (newGameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece) || !newGameState.IsWhiteTurn && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsBlack(piece)) {
                                    kingFile = i;
                                    kingRank = j;
                                    break;
                                }
                            }
                        }

                        if (kingFile != null) {
                            break;
                        }
                    }

                    var isCheck = ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(newGameState.Board, newGameState.Abilities, newGameState.IsWhiteTurn, newGameState.IsPlayerWhite, System.Nullable.getValue(kingFile), System.Nullable.getValue(kingRank));

                    return new ChessCompStompWithHacksEngine.MoveNaming.MoveResult(newGameState, isCheck, false);
                },
                GetRankName: function (rank) {
                    switch (rank) {
                        case 0: 
                            return "1";
                        case 1: 
                            return "2";
                        case 2: 
                            return "3";
                        case 3: 
                            return "4";
                        case 4: 
                            return "5";
                        case 5: 
                            return "6";
                        case 6: 
                            return "7";
                        case 7: 
                            return "8";
                        default: 
                            throw new System.Exception();
                    }
                },
                GetFileName: function (file) {
                    switch (file) {
                        case 0: 
                            return "a";
                        case 1: 
                            return "b";
                        case 2: 
                            return "c";
                        case 3: 
                            return "d";
                        case 4: 
                            return "e";
                        case 5: 
                            return "f";
                        case 6: 
                            return "g";
                        case 7: 
                            return "h";
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.MoveNaming.MoveResult", {
        $kind: "nested class",
        fields: {
            NewGameState: null,
            IsCheck: false,
            IsCheckmate: false
        },
        ctors: {
            ctor: function (newGameState, isCheck, isCheckmate) {
                this.$initialize();
                this.NewGameState = newGameState;
                this.IsCheck = isCheck;
                this.IsCheckmate = isCheckmate;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.MoveUtil", {
        statics: {
            methods: {
                IsCastlingOrSuperCastling: function (move, originalBoard) {
                    if (move.IsNuke) {
                        return false;
                    }

                    if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKing(originalBoard.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank))) === false) {
                        return false;
                    }

                    return Math.abs(((System.Nullable.getValue(move.StartingFile) - move.EndingFile) | 0)) === 2 || Math.abs(((System.Nullable.getValue(move.StartingRank) - move.EndingRank) | 0)) === 2;
                },
                IsCapturingMove: function (move, originalBoard) {
                    if (move.IsNuke) {
                        return false;
                    }

                    if (ChessCompStompWithHacksEngine.MoveUtil.IsCastlingOrSuperCastling(move, originalBoard)) {
                        return false;
                    }

                    if (originalBoard.GetPiece$1(move.EndingFile, move.EndingRank) !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                        return true;
                    }

                    return ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(originalBoard.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank))) && System.Nullable.getValue(move.StartingFile) !== move.EndingFile;
                },
                IsPawnMove: function (move, originalBoard) {
                    if (move.IsNuke) {
                        return false;
                    }

                    return ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsPawn(originalBoard.GetPiece$1(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank)));
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.Objective", {
        $kind: "enum",
        statics: {
            fields: {
                DefeatComputer: 0,
                DefeatComputerByPlayingAtMost25Moves: 1,
                DefeatComputerWith5QueensOnTheBoard: 2,
                CheckmateUsingAKnight: 3,
                PromoteAPieceToABishop: 4,
                LaunchANuke: 5,
                WinFinalBattle: 6
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.ObjectiveChecker", {
        statics: {
            methods: {
                GetCompletedObjectives: function (originalGameState, move, isFinalBattle) {
                    var completedObjectives = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Objective)).ctor();

                    if (move.IsNuke) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.LaunchANuke);
                    }

                    if (originalGameState.IsPlayerTurn() && System.Nullable.hasValue(move.Promotion) && System.Nullable.getValue(move.Promotion) === ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToBishop) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.PromoteAPieceToABishop);
                    }

                    var newGameState = ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(originalGameState, move);
                    var newGameStatus = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(newGameState).GameStatus;

                    var hasPlayerWon = newGameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory && newGameState.IsPlayerWhite || newGameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory && !newGameState.IsPlayerWhite;

                    if (hasPlayerWon) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.DefeatComputer);
                    }

                    if (hasPlayerWon && isFinalBattle) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.WinFinalBattle);
                    }

                    var numberOfMovesPlayedByPlayer;
                    if (newGameState.IsPlayerWhite) {
                        numberOfMovesPlayedByPlayer = (Bridge.Int.div(newGameState.TurnCount, 2)) | 0;
                    } else {
                        numberOfMovesPlayedByPlayer = (Bridge.Int.div((((newGameState.TurnCount - 1) | 0)), 2)) | 0;
                    }

                    if (hasPlayerWon && numberOfMovesPlayedByPlayer <= 25) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.DefeatComputerByPlayingAtMost25Moves);
                    }

                    if (hasPlayerWon && ChessCompStompWithHacksEngine.ObjectiveChecker.AtLeast5QueensOnTheBoard(newGameState.Board)) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.DefeatComputerWith5QueensOnTheBoard);
                    }

                    if (ChessCompStompWithHacksEngine.ObjectiveChecker.HasPlayerDeliveredCheckmateUsingAKnight(hasPlayerWon, newGameState)) {
                        completedObjectives.add(ChessCompStompWithHacksEngine.Objective.CheckmateUsingAKnight);
                    }

                    return completedObjectives;
                },
                AtLeast5QueensOnTheBoard: function (board) {
                    var count = 0;
                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsQueen(board.GetPiece$1(i, j))) {
                                count = (count + 1) | 0;
                            }
                        }
                    }

                    return count >= 5;
                },
                HasPlayerDeliveredCheckmateUsingAKnight: function (hasPlayerWon, gameState) {
                    var $t;
                    if (!hasPlayerWon) {
                        return false;
                    }

                    var playerKnight = gameState.IsPlayerWhite ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                    var enemyKing = gameState.IsPlayerWhite ? ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing : ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing;

                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            if (gameState.Board.GetPiece$1(i, j) === enemyKing) {
                                var knightMoves = new (System.Collections.Generic.List$1(System.Tuple$2(System.Int32,System.Int32))).ctor();
                                knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 2) | 0) });
                                knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 2) | 0) });
                                knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 2) | 0) });
                                knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 2) | 0) });
                                knightMoves.add({ Item1: ((i + 2) | 0), Item2: ((j + 1) | 0) });
                                knightMoves.add({ Item1: ((i + 2) | 0), Item2: ((j - 1) | 0) });
                                knightMoves.add({ Item1: ((i - 2) | 0), Item2: ((j + 1) | 0) });
                                knightMoves.add({ Item1: ((i - 2) | 0), Item2: ((j - 1) | 0) });
                                if (gameState.Abilities.CanKnightsMakeLargeKnightsMove) {
                                    knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j + 3) | 0) });
                                    knightMoves.add({ Item1: ((i + 1) | 0), Item2: ((j - 3) | 0) });
                                    knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j + 3) | 0) });
                                    knightMoves.add({ Item1: ((i - 1) | 0), Item2: ((j - 3) | 0) });
                                    knightMoves.add({ Item1: ((i + 3) | 0), Item2: ((j + 1) | 0) });
                                    knightMoves.add({ Item1: ((i + 3) | 0), Item2: ((j - 1) | 0) });
                                    knightMoves.add({ Item1: ((i - 3) | 0), Item2: ((j + 1) | 0) });
                                    knightMoves.add({ Item1: ((i - 3) | 0), Item2: ((j - 1) | 0) });
                                }

                                $t = Bridge.getEnumerator(knightMoves);
                                try {
                                    while ($t.moveNext()) {
                                        var knightMove = $t.Current;
                                        if (0 <= knightMove.Item1 && knightMove.Item1 < 8 && 0 <= knightMove.Item2 && knightMove.Item2 < 8) {
                                            if (gameState.Board.GetPiece$1(knightMove.Item1, knightMove.Item2) === playerKnight) {
                                                return true;
                                            }
                                        }
                                    }
                                } finally {
                                    if (Bridge.is($t, System.IDisposable)) {
                                        $t.System$IDisposable$Dispose();
                                    }
                                }
                            }
                        }
                    }

                    return false;
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.PromotionTypeUtil", {
        statics: {
            methods: {
                GetPromotedPiece: function (promotionType, isWhite) {
                    switch (promotionType) {
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToRook: 
                            return isWhite ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToKnight: 
                            return isWhite ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToBishop: 
                            return isWhite ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen: 
                            return isWhite ? ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen : ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.TacticalNukeUtil", {
        statics: {
            fields: {
                NumberOfMovesPlayedBeforeNukeIsAvailable: 0
            },
            ctors: {
                init: function () {
                    this.NumberOfMovesPlayedBeforeNukeIsAvailable = 10;
                }
            },
            methods: {
                GetNukedSquares: function (chessSquare) {
                    return ChessCompStompWithHacksEngine.TacticalNukeUtil.GetNukedSquares$1(chessSquare.File, chessSquare.Rank);
                },
                GetNukedSquares$1: function (file, rank) {
                    if (file < 0 || file >= 8) {
                        throw new System.Exception("File not in range: " + (DTLibrary.StringUtil.ToStringCultureInvariant(file) || ""));
                    }

                    if (rank < 0 || rank >= 8) {
                        throw new System.Exception("Rank not in range: " + (DTLibrary.StringUtil.ToStringCultureInvariant(rank) || ""));
                    }

                    var list = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ChessSquare)).ctor();

                    ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, ((file - 3) | 0), rank);
                    ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, ((file + 3) | 0), rank);

                    for (var j = -1; j <= 1; j = (j + 1) | 0) {
                        ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, ((file - 2) | 0), ((rank + j) | 0));
                        ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, ((file + 2) | 0), ((rank + j) | 0));
                    }

                    for (var j1 = -2; j1 <= 2; j1 = (j1 + 1) | 0) {
                        ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, ((file - 1) | 0), ((rank + j1) | 0));
                        ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, ((file + 1) | 0), ((rank + j1) | 0));
                    }

                    for (var j2 = -3; j2 <= 3; j2 = (j2 + 1) | 0) {
                        ChessCompStompWithHacksEngine.TacticalNukeUtil.TryAddSquare(list, file, ((rank + j2) | 0));
                    }

                    return list;
                },
                TryAddSquare: function (list, file, rank) {
                    if (0 <= file && file < 8 && 0 <= rank && rank < 8) {
                        list.add(new ChessCompStompWithHacksEngine.ChessSquare(file, rank));
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.UnmovedPawnsArray", {
        statics: {
            methods: {
                CopyBoard: function (board) {
                    var $t, $t1;
                    var newBoard = System.Array.init(8, null, System.Array.type(System.Boolean));
                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        newBoard[System.Array.index(i, newBoard)] = System.Array.init(8, false, System.Boolean);
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            ($t = newBoard[System.Array.index(i, newBoard)])[System.Array.index(j, $t)] = ($t1 = board[System.Array.index(i, board)])[System.Array.index(j, $t1)];
                        }
                    }

                    return newBoard;
                }
            }
        },
        fields: {
            board: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
            },
            $ctor1: function (board) {
                this.$initialize();
                this.board = ChessCompStompWithHacksEngine.UnmovedPawnsArray.CopyBoard(board);
            }
        },
        methods: {
            HasUnmovedPawn: function (file, rank) {
                var $t;
                return ($t = this.board[System.Array.index(file, this.board)])[System.Array.index(rank, $t)];
            },
            PawnMoved: function (file, rank) {
                var $t, $t1, $t2;
                var newBoard = System.Array.init(8, null, System.Array.type(System.Boolean));

                for (var i = 0; i < 8; i = (i + 1) | 0) {
                    if (i !== file) {
                        newBoard[System.Array.index(i, newBoard)] = this.board[System.Array.index(i, this.board)];
                    } else {
                        newBoard[System.Array.index(i, newBoard)] = System.Array.init(8, false, System.Boolean);
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            ($t = newBoard[System.Array.index(i, newBoard)])[System.Array.index(j, $t)] = ($t1 = this.board[System.Array.index(i, this.board)])[System.Array.index(j, $t1)];
                        }
                        ($t2 = newBoard[System.Array.index(i, newBoard)])[System.Array.index(rank, $t2)] = false;
                    }
                }

                var returnValue = new ChessCompStompWithHacksEngine.UnmovedPawnsArray.ctor();
                returnValue.board = newBoard;

                return returnValue;
            }
        }
    });

    Bridge.definei("DTLibrary.IFrame$4", function (ImageEnum, FontEnum, SoundEnum, MusicEnum) { return {
        $kind: "interface"
    }; });

    Bridge.define("ChessCompStompWithHacksLibrary.Button", {
        fields: {
            x: 0,
            y: 0,
            width: 0,
            height: 0,
            backgroundColor: null,
            hoverColor: null,
            clickColor: null,
            text: null,
            textXOffset: 0,
            textYOffset: 0,
            font: 0,
            isHover: false,
            isClicked: false
        },
        ctors: {
            ctor: function (x, y, width, height, backgroundColor, hoverColor, clickColor, text, textXOffset, textYOffset, font) {
                this.$initialize();
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.backgroundColor = backgroundColor;
                this.hoverColor = hoverColor;
                this.clickColor = clickColor;
                this.text = text;
                this.textXOffset = textXOffset;
                this.textYOffset = textYOffset;
                this.font = font;

                this.isHover = false;
                this.isClicked = false;
            }
        },
        methods: {
            IsHover: function (mouseInput) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();
                return this.x <= mouseX && mouseX <= ((this.x + this.width) | 0) && this.y <= mouseY && mouseY <= ((this.y + this.height) | 0);
            },
            /**
             * Returns whether or not the user has clicked the button
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.Button
             * @memberof ChessCompStompWithHacksLibrary.Button
             * @param   {DTLibrary.IMouse}    mouseInput            
             * @param   {DTLibrary.IMouse}    previousMouseInput
             * @return  {boolean}
             */
            ProcessFrame: function (mouseInput, previousMouseInput) {
                var inRange = this.IsHover(mouseInput);

                this.isHover = inRange;

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    if (inRange) {
                        this.isClicked = true;
                    }
                }

                if (this.isClicked && !mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.isClicked = false;

                    if (inRange) {
                        return true;
                    }
                }

                return false;
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, ((this.width - 1) | 0), ((this.height - 1) | 0), this.isClicked ? this.clickColor : (this.isHover ? this.hoverColor : this.backgroundColor), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, this.width, this.height, DTLibrary.DTColor.Black(), false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + this.textXOffset) | 0), ((((this.y + this.height) | 0) - this.textYOffset) | 0), this.text, this.font, DTLibrary.DTColor.Black());
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessCompStompWithHacks", {
        statics: {
            fields: {
                WINDOW_WIDTH: 0,
                WINDOW_HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.WINDOW_WIDTH = 1000;
                    this.WINDOW_HEIGHT = 700;
                }
            },
            methods: {
                GetFirstFrame: function (globalState) {
                    var frame = new ChessCompStompWithHacksLibrary.InitialLoadingScreenFrame(globalState);
                    return frame;
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessFontUtil", {
        statics: {
            methods: {
                GetFontInfo: function (font) {
                    switch (font) {
                        case ChessCompStompWithHacksLibrary.ChessFont.Fetamont12Pt: 
                            return new ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo("Metaflop/Fetamont.ttf", "Metaflop/Fetamont.woff", "Fetamont", 12, "15.86", "15.5");
                        case ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt: 
                            return new ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo("Metaflop/Fetamont.ttf", "Metaflop/Fetamont.woff", "Fetamont", 14, "19.31", "18.5");
                        case ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt: 
                            return new ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo("Metaflop/Fetamont.ttf", "Metaflop/Fetamont.woff", "Fetamont", 16, "21.85", "23");
                        case ChessCompStompWithHacksLibrary.ChessFont.Fetamont18Pt: 
                            return new ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo("Metaflop/Fetamont.ttf", "Metaflop/Fetamont.woff", "Fetamont", 18, "24.19", "24");
                        case ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt: 
                            return new ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo("Metaflop/Fetamont.ttf", "Metaflop/Fetamont.woff", "Fetamont", 20, "26.76", "28.2");
                        case ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt: 
                            return new ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo("Metaflop/Fetamont.ttf", "Metaflop/Fetamont.woff", "Fetamont", 32, "42.95", "44");
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessFontUtil.FontInfo", {
        $kind: "nested class",
        fields: {
            TtfFontFilename: null,
            WoffFontFilename: null,
            FontFamilyName: null,
            FontSize: 0,
            JavascriptFontSize: null,
            JavascriptLineHeight: null
        },
        ctors: {
            ctor: function (ttfFontFilename, woffFontFilename, fontFamilyName, fontSize, javascriptFontSize, javascriptLineHeight) {
                this.$initialize();
                this.TtfFontFilename = ttfFontFilename;
                this.WoffFontFilename = woffFontFilename;
                this.FontFamilyName = fontFamilyName;
                this.FontSize = fontSize;
                this.JavascriptFontSize = javascriptFontSize;
                this.JavascriptLineHeight = javascriptLineHeight;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessImageUtil", {
        statics: {
            fields: {
                ChessPieceScalingFactor: 0
            },
            ctors: {
                init: function () {
                    this.ChessPieceScalingFactor = 16;
                }
            },
            methods: {
                GetImage: function (piece) {
                    switch (piece) {
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn: 
                            return ChessCompStompWithHacksLibrary.ChessImage.BlackPawn;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight: 
                            return ChessCompStompWithHacksLibrary.ChessImage.BlackKnight;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop: 
                            return ChessCompStompWithHacksLibrary.ChessImage.BlackBishop;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook: 
                            return ChessCompStompWithHacksLibrary.ChessImage.BlackRook;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen: 
                            return ChessCompStompWithHacksLibrary.ChessImage.BlackQueen;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing: 
                            return ChessCompStompWithHacksLibrary.ChessImage.BlackKing;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn: 
                            return ChessCompStompWithHacksLibrary.ChessImage.WhitePawn;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight: 
                            return ChessCompStompWithHacksLibrary.ChessImage.WhiteKnight;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop: 
                            return ChessCompStompWithHacksLibrary.ChessImage.WhiteBishop;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook: 
                            return ChessCompStompWithHacksLibrary.ChessImage.WhiteRook;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen: 
                            return ChessCompStompWithHacksLibrary.ChessImage.WhiteQueen;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing: 
                            return ChessCompStompWithHacksLibrary.ChessImage.WhiteKing;
                        case ChessCompStompWithHacksEngine.ChessSquarePiece.Empty: 
                            throw new System.Exception();
                        default: 
                            throw new System.Exception();
                    }
                },
                GetImageFilename: function (image) {
                    switch (image) {
                        case ChessCompStompWithHacksLibrary.ChessImage.SoundOn: 
                            return "Kenney/SoundOn.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.SoundOff: 
                            return "Kenney/SoundOff.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.MusicOn: 
                            return "Kenney/MusicOn.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.MusicOff: 
                            return "Kenney/MusicOff.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Gear: 
                            return "Kenney/Gear.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.GearHover: 
                            return "Kenney/Gear_Hover.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.GearSelected: 
                            return "Kenney/Gear_Selected.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.BlackPawn: 
                            return "Cburnett/BlackPawn.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.BlackRook: 
                            return "Cburnett/BlackRook.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.BlackKnight: 
                            return "Cburnett/BlackKnight.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.BlackBishop: 
                            return "Cburnett/BlackBishop.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.BlackQueen: 
                            return "Cburnett/BlackQueen.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.BlackKing: 
                            return "Cburnett/BlackKing.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.WhitePawn: 
                            return "Cburnett/WhitePawn.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.WhiteRook: 
                            return "Cburnett/WhiteRook.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.WhiteKnight: 
                            return "Cburnett/WhiteKnight.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.WhiteBishop: 
                            return "Cburnett/WhiteBishop.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.WhiteQueen: 
                            return "Cburnett/WhiteQueen.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.WhiteKing: 
                            return "Cburnett/WhiteKing.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_NotReady: 
                            return "Kenney/spaceRockets_004.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready: 
                            return "Kenney/spaceRockets_004_green.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Hover: 
                            return "Kenney/spaceRockets_004_highlighted.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Selected: 
                            return "Kenney/spaceRockets_004_yellow.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire: 
                            return "Kenney/spaceEffects_004.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion1: 
                            return "Kenney/regularExplosion00.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion2: 
                            return "Kenney/regularExplosion01.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion3: 
                            return "Kenney/regularExplosion02.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion4: 
                            return "Kenney/regularExplosion03.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion5: 
                            return "Kenney/regularExplosion04.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion6: 
                            return "Kenney/regularExplosion05.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion7: 
                            return "Kenney/regularExplosion06.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion8: 
                            return "Kenney/regularExplosion07.png";
                        case ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion9: 
                            return "Kenney/regularExplosion08.png";
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessMusicUtil", {
        statics: {
            methods: {
                GetMusicFilename: function (music) {
                    switch (music) {
                        default: 
                            throw new System.Exception();
                    }
                },
                GetMusicVolume: function (music) {
                    switch (music) {
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessPiecesRenderer", {
        statics: {
            fields: {
                NUKE_BEGIN_LANDING_MICROSECONDS: 0,
                NUKE_IMPACT_MICROSECONDS: 0,
                NUKE_EXPLOSION_FINISHED_MICROSECONDS: 0,
                NUKE_ANIMATION_COMPLETED_MICROSECONDS: 0
            },
            ctors: {
                init: function () {
                    this.NUKE_BEGIN_LANDING_MICROSECONDS = 1000000;
                    this.NUKE_IMPACT_MICROSECONDS = 1300000;
                    this.NUKE_EXPLOSION_FINISHED_MICROSECONDS = 1600000;
                    this.NUKE_ANIMATION_COMPLETED_MICROSECONDS = 1650000;
                }
            },
            methods: {
                GetChessPiecesRenderer: function (pieces, kingInDangerSquare, previousMoveSquares, renderFromWhitePerspective) {
                    return new ChessCompStompWithHacksLibrary.ChessPiecesRenderer(pieces, kingInDangerSquare, previousMoveSquares, null, DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare).EmptyList(), null, null, null, renderFromWhitePerspective, null, null, null);
                },
                GetHoverSquare: function (mouseInput, renderFromWhitePerspective, displayProcessing) {
                    var width = (Bridge.Int.div(Bridge.Int.mul(displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;
                    var height = (Bridge.Int.div(Bridge.Int.mul(displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;

                    var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                    var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                    if (mouseX < 0 || mouseY < 0) {
                        return null;
                    }

                    var i = (Bridge.Int.div(mouseX, width)) | 0;
                    var j = (Bridge.Int.div(mouseY, height)) | 0;

                    if (0 <= i && i < 8 && 0 <= j && j < 8) {
                        if (renderFromWhitePerspective) {
                            return new ChessCompStompWithHacksEngine.ChessSquare(i, j);
                        } else {
                            return new ChessCompStompWithHacksEngine.ChessSquare(((7 - i) | 0), ((7 - j) | 0));
                        }
                    }

                    return null;
                },
                GetRenderSquare$1: function (i, j, renderFromWhitePerspective) {
                    if (renderFromWhitePerspective) {
                        return new ChessCompStompWithHacksEngine.ChessSquare(i, j);
                    }

                    return new ChessCompStompWithHacksEngine.ChessSquare(((7 - i) | 0), ((7 - j) | 0));
                },
                GetRenderSquare: function (square, renderFromWhitePerspective) {
                    return ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare$1(square.File, square.Rank, renderFromWhitePerspective);
                }
            }
        },
        fields: {
            pieces: null,
            kingInDangerSquare: null,
            previousMoveSquares: null,
            selectedPieceSquare: null,
            possibleMoveSquares: null,
            potentialNukeSquaresInfo: null,
            hoverSquare: null,
            hoverPieceInfo: null,
            renderFromWhitePerspective: false,
            nukeAnimationMicroseconds: null,
            nukeCenter: null,
            nukedSquares: null
        },
        ctors: {
            ctor: function (pieces, kingInDangerSquare, previousMoveSquares, selectedPieceSquare, possibleMoveSquares, potentialNukeSquaresInfo, hoverSquare, hoverPieceInfo, renderFromWhitePerspective, nukeAnimationMicroseconds, nukeCenter, nukedSquares) {
                this.$initialize();
                this.pieces = pieces;
                this.kingInDangerSquare = kingInDangerSquare;
                this.previousMoveSquares = previousMoveSquares;
                this.selectedPieceSquare = selectedPieceSquare;
                this.possibleMoveSquares = possibleMoveSquares;
                this.potentialNukeSquaresInfo = potentialNukeSquaresInfo;
                this.hoverSquare = hoverSquare;
                this.hoverPieceInfo = hoverPieceInfo;
                this.renderFromWhitePerspective = renderFromWhitePerspective;
                this.nukeAnimationMicroseconds = nukeAnimationMicroseconds;
                this.nukeCenter = nukeCenter;
                this.nukedSquares = nukedSquares;
            }
        },
        methods: {
            LandNuke: function (nukeCenter) {
                var nukedSquares = ChessCompStompWithHacksEngine.TacticalNukeUtil.GetNukedSquares(nukeCenter);

                return new ChessCompStompWithHacksLibrary.ChessPiecesRenderer(this.pieces, this.kingInDangerSquare, this.previousMoveSquares, this.selectedPieceSquare, this.possibleMoveSquares, this.potentialNukeSquaresInfo, this.hoverSquare, this.hoverPieceInfo, this.renderFromWhitePerspective, 0, nukeCenter, new (DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare)).$ctor2(nukedSquares));
            },
            ProcessFrame: function (pieces, kingInDangerSquare, previousMoveSquares, selectedPieceSquare, possibleMoveSquares, potentialNukeSquaresInfo, hoverSquare, hoverPieceInfo, elapsedMicrosPerFrame) {
                var newNukeAnimationMicroseconds;
                if (this.nukeAnimationMicroseconds == null) {
                    newNukeAnimationMicroseconds = null;
                } else {
                    newNukeAnimationMicroseconds = Bridge.Int.clip32(System.Nullable.getValue(this.nukeAnimationMicroseconds) + elapsedMicrosPerFrame);
                }

                if (System.Nullable.hasValue(newNukeAnimationMicroseconds) && System.Nullable.getValue(newNukeAnimationMicroseconds) > ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_ANIMATION_COMPLETED_MICROSECONDS) {
                    newNukeAnimationMicroseconds = 1650001;
                }

                return new ChessCompStompWithHacksLibrary.ChessPiecesRenderer(pieces, kingInDangerSquare, previousMoveSquares, selectedPieceSquare, possibleMoveSquares, potentialNukeSquaresInfo, hoverSquare, hoverPieceInfo, this.renderFromWhitePerspective, newNukeAnimationMicroseconds, this.nukeCenter, this.nukedSquares);
            },
            HasNukeLanded: function () {
                return System.Nullable.hasValue(this.nukeAnimationMicroseconds) && System.Nullable.getValue(this.nukeAnimationMicroseconds) >= ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_IMPACT_MICROSECONDS;
            },
            HasNukeFinished: function () {
                return System.Nullable.hasValue(this.nukeAnimationMicroseconds) && System.Nullable.getValue(this.nukeAnimationMicroseconds) >= ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_ANIMATION_COMPLETED_MICROSECONDS;
            },
            Render: function (displayOutput) {
                var width = (Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;
                var height = (Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;

                for (var i = 0; i < 8; i = (i + 1) | 0) {
                    for (var j = 0; j < 8; j = (j + 1) | 0) {
                        var renderSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare$1(i, j, this.renderFromWhitePerspective);

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(Bridge.Int.mul(renderSquare.File, width), Bridge.Int.mul(renderSquare.Rank, height), width, height, (((i + j) | 0)) % 2 === 0 ? new DTLibrary.DTColor.ctor(140, 89, 11) : new DTLibrary.DTColor.ctor(194, 146, 74), true);
                    }
                }

                for (var i1 = 0; i1 < this.previousMoveSquares.Count; i1 = (i1 + 1) | 0) {
                    var previousMoveSquare = this.previousMoveSquares.getItem(i1);
                    var renderSquare1 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare(previousMoveSquare, this.renderFromWhitePerspective);
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(Bridge.Int.mul(renderSquare1.File, width), Bridge.Int.mul(renderSquare1.Rank, height), width, height, new DTLibrary.DTColor.$ctor1(128, 128, 128, 128), true);
                }

                if (this.kingInDangerSquare != null) {
                    var renderSquare2 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare(this.kingInDangerSquare, this.renderFromWhitePerspective);
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(Bridge.Int.mul(renderSquare2.File, width), Bridge.Int.mul(renderSquare2.Rank, height), width, height, new DTLibrary.DTColor.$ctor1(255, 0, 0, 128), true);
                }

                for (var i2 = 0; i2 < 8; i2 = (i2 + 1) | 0) {
                    for (var j1 = 0; j1 < 8; j1 = (j1 + 1) | 0) {
                        var square = this.pieces.GetPiece$1(i2, j1);

                        var renderSquare3 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare$1(i2, j1, this.renderFromWhitePerspective);

                        if (square === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                            continue;
                        }

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(ChessCompStompWithHacksLibrary.ChessImageUtil.GetImage(square), Bridge.Int.mul(renderSquare3.File, width), Bridge.Int.mul(renderSquare3.Rank, height), 0, ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor);
                    }
                }

                if (this.selectedPieceSquare != null) {
                    var renderSquare4 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare(this.selectedPieceSquare, this.renderFromWhitePerspective);
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(Bridge.Int.mul(renderSquare4.File, width), Bridge.Int.mul(renderSquare4.Rank, height), width, height, new DTLibrary.DTColor.$ctor1(0, 128, 0, 128), true);
                }

                for (var i3 = 0; i3 < this.possibleMoveSquares.Count; i3 = (i3 + 1) | 0) {
                    var possibleMoveSquare = this.possibleMoveSquares.getItem(i3);
                    var renderSquare5 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare(possibleMoveSquare, this.renderFromWhitePerspective);
                    DTLibrary.DisplayExtensions.DrawThickRectangle(ChessCompStompWithHacksLibrary.ChessImage, ChessCompStompWithHacksLibrary.ChessFont, displayOutput, Bridge.Int.mul(renderSquare5.File, width), Bridge.Int.mul(renderSquare5.Rank, height), width, height, 1, new DTLibrary.DTColor.$ctor1(0, 128, 0, 128), false);
                }

                if (this.hoverSquare != null) {
                    var renderSquare6 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare(this.hoverSquare, this.renderFromWhitePerspective);
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(Bridge.Int.mul(renderSquare6.File, width), Bridge.Int.mul(renderSquare6.Rank, height), width, height, new DTLibrary.DTColor.$ctor1(0, 0, 128, 128), true);
                }

                if (this.potentialNukeSquaresInfo != null) {
                    for (var i4 = 0; i4 < this.potentialNukeSquaresInfo.PotentialNukeSquares.Count; i4 = (i4 + 1) | 0) {
                        var potentialNukeSquare = this.potentialNukeSquaresInfo.PotentialNukeSquares.getItem(i4);
                        var renderSquare7 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare(potentialNukeSquare, this.renderFromWhitePerspective);
                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(Bridge.Int.mul(renderSquare7.File, width), Bridge.Int.mul(renderSquare7.Rank, height), width, height, this.potentialNukeSquaresInfo.IsNukeLocationValid ? new DTLibrary.DTColor.$ctor1(0, 200, 0, 200) : new DTLibrary.DTColor.$ctor1(200, 0, 0, 200), true);
                    }
                }

                if (this.hoverPieceInfo != null) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(ChessCompStompWithHacksLibrary.ChessImageUtil.GetImage(this.hoverPieceInfo.ChessSquarePiece), ((this.hoverPieceInfo.X - ((Bridge.Int.div(width, 2)) | 0)) | 0), ((this.hoverPieceInfo.Y - ((Bridge.Int.div(height, 2)) | 0)) | 0), 0, ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor);
                }

                this.RenderNukeAnimation(displayOutput);
            },
            RenderNukeAnimation: function (displayOutput) {
                if (this.nukeAnimationMicroseconds == null) {
                    return;
                }

                var width = (Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;
                var height = (Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;

                if (System.Nullable.getValue(this.nukeAnimationMicroseconds) <= ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_IMPACT_MICROSECONDS) {
                    if (System.Nullable.getValue(this.nukeAnimationMicroseconds) >= ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_BEGIN_LANDING_MICROSECONDS) {
                        var nukeRenderCenter = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare$1(this.nukeCenter.File, this.nukeCenter.Rank, this.renderFromWhitePerspective);

                        var rocketWidth = displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready);

                        var rocketFireScalingFactor = 256;
                        var rocketFireWidthOriginal = displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire);
                        var rocketFireHeightOriginal = displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire);
                        var rocketFireWidthScaled = (Bridge.Int.div(Bridge.Int.mul(rocketFireWidthOriginal, rocketFireScalingFactor), 128)) | 0;
                        var rocketFireHeightScaled = (Bridge.Int.div(Bridge.Int.mul(rocketFireHeightOriginal, rocketFireScalingFactor), 128)) | 0;
                        var endingY = (Bridge.Int.mul(nukeRenderCenter.Rank, height) + ((Bridge.Int.div(height, 2)) | 0)) | 0;
                        var startingY = (endingY + ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT) | 0;
                        var totalDistanceY = (startingY - endingY) | 0;
                        var y = System.Int64.clip32(((System.Int64(1300000)).sub(System.Int64(System.Nullable.getValue(this.nukeAnimationMicroseconds)))).mul(System.Int64(totalDistanceY)).div((System.Int64(300000))).add(System.Int64(endingY)));

                        var x = (Bridge.Int.mul(nukeRenderCenter.File, width) + ((Bridge.Int.div(width, 2)) | 0)) | 0;

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready, ((x - (displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready) >> 1)) | 0), y, 23040, 128);

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire, ((((x - (displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready) >> 1)) | 0) + ((Bridge.Int.div((((rocketWidth - rocketFireWidthScaled) | 0)), 2)) | 0)) | 0), ((y + displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready)) | 0), 23040, rocketFireScalingFactor);
                    }
                } else if (System.Nullable.getValue(this.nukeAnimationMicroseconds) <= ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_EXPLOSION_FINISHED_MICROSECONDS) {
                    var elapsedTime = (System.Nullable.getValue(this.nukeAnimationMicroseconds) - ChessCompStompWithHacksLibrary.ChessPiecesRenderer.NUKE_IMPACT_MICROSECONDS) | 0;
                    var totalTime = 300000;

                    var spriteNum = (((Bridge.Int.div(Bridge.Int.mul(elapsedTime, 9), totalTime)) | 0) + 1) | 0;
                    if (spriteNum < 1) {
                        spriteNum = 1;
                    }
                    if (spriteNum > 9) {
                        spriteNum = 9;
                    }

                    var explosionImage = new ChessCompStompWithHacksLibrary.ChessImage();

                    switch (spriteNum) {
                        case 1: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion1;
                            break;
                        case 2: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion2;
                            break;
                        case 3: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion3;
                            break;
                        case 4: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion4;
                            break;
                        case 5: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion5;
                            break;
                        case 6: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion6;
                            break;
                        case 7: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion7;
                            break;
                        case 8: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion8;
                            break;
                        case 9: 
                            explosionImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Explosion9;
                            break;
                        default: 
                            throw new System.Exception();
                    }

                    var nukeRenderCenter1 = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetRenderSquare$1(this.nukeCenter.File, this.nukeCenter.Rank, this.renderFromWhitePerspective);

                    var explosionScalingFactor = 256;

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(explosionImage, ((((Bridge.Int.mul(nukeRenderCenter1.File, width) + ((Bridge.Int.div(width, 2)) | 0)) | 0) - ((Bridge.Int.div(((Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(explosionImage), explosionScalingFactor), 128)) | 0), 2)) | 0)) | 0), ((((Bridge.Int.mul(nukeRenderCenter1.Rank, height) + ((Bridge.Int.div(height, 2)) | 0)) | 0) - ((Bridge.Int.div(((Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(explosionImage), explosionScalingFactor), 128)) | 0), 2)) | 0)) | 0), 0, explosionScalingFactor);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessPiecesRenderer.HoverPieceInfo", {
        $kind: "nested class",
        fields: {
            ChessSquarePiece: 0,
            X: 0,
            Y: 0
        },
        ctors: {
            ctor: function (chessSquarePiece, x, y) {
                this.$initialize();
                this.ChessSquarePiece = chessSquarePiece;
                this.X = x;
                this.Y = y;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessPiecesRenderer.PotentialNukeSquaresInfo", {
        $kind: "nested class",
        fields: {
            PotentialNukeSquares: null,
            IsNukeLocationValid: false
        },
        ctors: {
            ctor: function (nukeCenter, isNukeLocationValid) {
                this.$initialize();
                var potentialNukedSquares = ChessCompStompWithHacksEngine.TacticalNukeUtil.GetNukedSquares(nukeCenter);

                this.PotentialNukeSquares = new (DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare)).$ctor2(potentialNukedSquares);
                this.IsNukeLocationValid = isNukeLocationValid;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessPiecesRendererUtil", {
        statics: {
            methods: {
                GetKingInDangerSquare: function (gameState) {
                    var kingFile = null;
                    var kingRank = null;

                    for (var i = 0; i < 8; i = (i + 1) | 0) {
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsKing(gameState.Board.GetPiece$1(i, j)) && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(gameState.Board.GetPiece$1(i, j)) === gameState.IsWhiteTurn) {
                                kingFile = i;
                                kingRank = j;
                                break;
                            }
                        }

                        if (System.Nullable.hasValue(kingFile)) {
                            break;
                        }
                    }

                    var isKingUnderAttack = ChessCompStompWithHacksEngine.CheckKingUnderAttack.IsKingUnderThreat(gameState.Board, gameState.Abilities, gameState.IsWhiteTurn, gameState.IsPlayerWhite, System.Nullable.getValue(kingFile), System.Nullable.getValue(kingRank));

                    if (isKingUnderAttack) {
                        return new ChessCompStompWithHacksEngine.ChessSquare(System.Nullable.getValue(kingFile), System.Nullable.getValue(kingRank));
                    }
                    return null;
                },
                GetPreviousMoveSquares: function (originalGameState, displayMove) {
                    return ChessCompStompWithHacksLibrary.ChessPiecesRendererUtil.GetPreviousMoveSquares$1(originalGameState, displayMove.Move);
                },
                GetPreviousMoveSquares$1: function (originalGameState, move) {
                    if (move.IsNuke) {
                        var nukedSquares = ChessCompStompWithHacksEngine.TacticalNukeUtil.GetNukedSquares$1(move.EndingFile, move.EndingRank);

                        return DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare).AsImmutableList(nukedSquares);
                    }

                    if (ChessCompStompWithHacksEngine.MoveUtil.IsCastlingOrSuperCastling(move, originalGameState.Board)) {
                        var squares = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.ChessSquare)).ctor();

                        var king = new ChessCompStompWithHacksEngine.ChessSquare(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank));

                        var direction;
                        if (((move.EndingFile - System.Nullable.getValue(move.StartingFile)) | 0) === 2) {
                            direction = { Item1: 1, Item2: 0 };
                        } else {
                            if (((move.EndingFile - System.Nullable.getValue(move.StartingFile)) | 0) === -2) {
                                direction = { Item1: -1, Item2: 0 };
                            } else {
                                if (((move.EndingRank - System.Nullable.getValue(move.StartingRank)) | 0) === 2) {
                                    direction = { Item1: 0, Item2: 1 };
                                } else {
                                    if (((move.EndingRank - System.Nullable.getValue(move.StartingRank)) | 0) === -2) {
                                        direction = { Item1: 0, Item2: -1 };
                                    } else {
                                        throw new System.Exception();
                                    }
                                }
                            }
                        }

                        var rook = king;
                        while (true) {
                            rook = new ChessCompStompWithHacksEngine.ChessSquare(((rook.File + direction.Item1) | 0), ((rook.Rank + direction.Item2) | 0));
                            if (ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsRook(originalGameState.Board.GetPiece(rook))) {
                                break;
                            }
                        }

                        squares.add(king);
                        squares.add(rook);
                        squares.add(new ChessCompStompWithHacksEngine.ChessSquare(move.EndingFile, move.EndingRank));
                        squares.add(new ChessCompStompWithHacksEngine.ChessSquare(((Bridge.Int.div((((System.Nullable.getValue(move.StartingFile) + move.EndingFile) | 0)), 2)) | 0), ((Bridge.Int.div((((System.Nullable.getValue(move.StartingRank) + move.EndingRank) | 0)), 2)) | 0)));

                        return new (DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare)).$ctor1(squares);
                    } else {
                        var squares1 = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.ChessSquare)).ctor();
                        squares1.add(new ChessCompStompWithHacksEngine.ChessSquare(System.Nullable.getValue(move.StartingFile), System.Nullable.getValue(move.StartingRank)));
                        squares1.add(new ChessCompStompWithHacksEngine.ChessSquare(move.EndingFile, move.EndingRank));

                        return DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare).AsImmutableList(squares1);
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessSoundUtil", {
        statics: {
            methods: {
                GetSoundFilename: function (sound) {
                    switch (sound) {
                        default: 
                            throw new System.Exception();
                    }
                },
                GetSoundVolume: function (sound) {
                    switch (sound) {
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.Credits_DesignAndCoding", {
        statics: {
            methods: {
                GetWebBrowserVersionText: function () {
                    return "Design and coding by dtsudo (https://github.com/dtsudo)\n\nThis game is open source, under the MIT license.\n\nThe source code is written in C#.\n\nBridge.NET is used to transpile the C# source code into javascript.\nBridge.NET is licensed under Apache License 2.0.\n(https://github.com/bridgedotnet/Bridge)";
                },
                GetDesktopVersionText: function () {
                    return "";
                },
                Render: function (displayOutput, width, height, isWebBrowserVersion) {
                    var text = isWebBrowserVersion ? ChessCompStompWithHacksLibrary.Credits_DesignAndCoding.GetWebBrowserVersionText() : ChessCompStompWithHacksLibrary.Credits_DesignAndCoding.GetDesktopVersionText();

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(10, ((height - 10) | 0), text, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.Credits_Font", {
        statics: {
            methods: {
                Render: function (displayOutput, width, height) {
                    var text = "The font used in this game was generated by metaflop.\nhttps://www.metaflop.com/modulator\n\nThe font is licensed under SIL Open Font License v1.1";

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(10, ((height - 10) | 0), text, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.Credits_Images", {
        statics: {
            methods: {
                Render: function (displayOutput, width, height) {
                    var text = "The images of chess pieces were created by Cburnett\n(https://en.wikipedia.org/wiki/User:Cburnett) and are licensed under\nthe BSD license.\n\nThe game also uses sprites from Kenney Asset Pack.\nThese sprites are licensed under Creative Commons Zero.\n(https://www.kenney.nl)";

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(10, ((height - 10) | 0), text, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.Credits_Music", {
        statics: {
            methods: {
                GetText: function () {
                    return "";
                },
                Render: function (displayOutput, width, height) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(10, ((height - 10) | 0), ChessCompStompWithHacksLibrary.Credits_Music.GetText(), ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.Credits_Sound", {
        statics: {
            methods: {
                GetText: function () {
                    return "";
                },
                Render: function (displayOutput, width, height) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(10, ((height - 10) | 0), ChessCompStompWithHacksLibrary.Credits_Sound.GetText(), ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.CreditsFrame.Tab", {
        $kind: "nested enum",
        statics: {
            fields: {
                DesignAndCoding: 0,
                Images: 1,
                Font: 2,
                Sound: 3,
                Music: 4
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.CreditsFrame.TabButton", {
        $kind: "nested class",
        fields: {
            X: 0,
            Y: 0,
            Width: 0,
            Height: 0,
            Tab: 0,
            TabName: null
        },
        ctors: {
            ctor: function (x, y, width, height, tab, tabName) {
                this.$initialize();
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
                this.Tab = tab;
                this.TabName = tabName;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel", {
        statics: {
            fields: {
                WIDTH: 0,
                HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.WIDTH = 850;
                    this.HEIGHT = 300;
                }
            }
        },
        fields: {
            x: 0,
            y: 0,
            mouseDragXStart: null,
            mouseDragYStart: null,
            continueButton: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                this.x = 75;
                this.y = 200;

                this.mouseDragXStart = null;
                this.mouseDragYStart = null;

                this.continueButton = new ChessCompStompWithHacksLibrary.Button(350, 37, 150, 40, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "OK", 57, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            ProcessFrame: function (mouseInput, previousMouseInput) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                var translatedMouse = new DTLibrary.TranslatedMouse(mouseInput, ((-this.x) | 0), ((-this.y) | 0));

                var isHoverOverPanel = this.x <= mouseX && mouseX <= ((this.x + ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel.WIDTH) | 0) && this.y <= mouseY && mouseY <= ((this.y + ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel.HEIGHT) | 0);

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && isHoverOverPanel && !this.continueButton.IsHover(translatedMouse)) {
                    this.mouseDragXStart = mouseX;
                    this.mouseDragYStart = mouseY;
                }

                if (this.mouseDragXStart != null && mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.x = (this.x + (((mouseX - System.Nullable.getValue(this.mouseDragXStart)) | 0))) | 0;
                    this.y = (this.y + (((mouseY - System.Nullable.getValue(this.mouseDragYStart)) | 0))) | 0;

                    this.mouseDragXStart = mouseX;
                    this.mouseDragYStart = mouseY;
                }

                if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.mouseDragXStart = null;
                    this.mouseDragYStart = null;

                    if (this.x < 0) {
                        this.x = 0;
                    }

                    if (this.y < 0) {
                        this.y = 0;
                    }

                    if (this.x > 150) {
                        this.x = 150;
                    }

                    if (this.y > 400) {
                        this.y = 400;
                    }
                }

                var isClicked = this.continueButton.ProcessFrame(translatedMouse, new DTLibrary.TranslatedMouse(previousMouseInput, ((-this.x) | 0), ((-this.y) | 0)));

                return new ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel.Result(isClicked, isHoverOverPanel || this.mouseDragXStart != null);
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, 849, 299, DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel.WIDTH, ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel.HEIGHT, DTLibrary.DTColor.Black(), false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + 335) | 0), ((this.y + 270) | 0), "You Win!", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + 47) | 0), ((this.y + 183) | 0), "You've defeated the AI in the Final Battle.\nYou are an Elite Hacker and an Elite Chess Grandmaster!", ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());

                this.continueButton.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, this.x, this.y));
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel.Result", {
        $kind: "nested class",
        fields: {
            HasClickedContinueButton: false,
            IsHoverOverPanel: false
        },
        ctors: {
            ctor: function (hasClickedContinueButton, isHoverOverPanel) {
                this.$initialize();
                this.HasClickedContinueButton = hasClickedContinueButton;
                this.IsHoverOverPanel = isHoverOverPanel;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GameLogic", {
        statics: {
            fields: {
                CHESS_PIECES_RENDERER_X: 0,
                CHESS_PIECES_RENDERER_Y: 0,
                NUKE_RENDERER_X: 0,
                NUKE_RENDERER_Y: 0,
                MOVE_TRACKER_RENDERER_X: 0,
                MOVE_TRACKER_RENDERER_Y: 0
            },
            ctors: {
                init: function () {
                    this.CHESS_PIECES_RENDERER_X = 186;
                    this.CHESS_PIECES_RENDERER_Y = 50;
                    this.NUKE_RENDERER_X = 25;
                    this.NUKE_RENDERER_Y = 50;
                    this.MOVE_TRACKER_RENDERER_X = 720;
                    this.MOVE_TRACKER_RENDERER_Y = 208;
                }
            },
            methods: {
                GetPlayerMove: function (mouseInput, previousMouseInput, isPromotionPanelOpen, clickedPromotionPiece, possibleMoves, promotionMoves, isPlayerWhite, isNukeInFlight, promotionPanelX, promotionPanelY, clickedSquare, clickedAndHeldSquare, hasClickedOnNuke, hasClickedAndHeldOnNuke, displayProcessing) {
                    var hoverSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetHoverSquare(new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput), isPlayerWhite, displayProcessing);

                    if (isNukeInFlight) {
                        return null;
                    }

                    if (isPromotionPanelOpen) {
                        if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                            if (clickedPromotionPiece != null) {
                                var hoverOverSquare = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverSquare(promotionPanelX, promotionPanelY, mouseInput, displayProcessing);

                                if (hoverOverSquare != null && System.Nullable.getValue(clickedPromotionPiece) === System.Nullable.getValue(hoverOverSquare)) {
                                    return System.Linq.Enumerable.from(promotionMoves).single(function (x) {
                                            return System.Nullable.getValue(x.Promotion) === System.Nullable.getValue(hoverOverSquare);
                                        });
                                }
                            }
                        }
                        return null;
                    }

                    if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (clickedSquare == null && clickedAndHeldSquare != null && hoverSquare != null && !hasClickedOnNuke) {
                            var moves = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedAndHeldSquare.File && System.Nullable.getValue(x.StartingRank) === clickedAndHeldSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves.Count === 1) {
                                return moves.getItem(0);
                            }
                        }

                        if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.equalsT(clickedAndHeldSquare)) {
                            var moves1 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedSquare.File && System.Nullable.getValue(x.StartingRank) === clickedSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves1.Count === 1) {
                                return moves1.getItem(0);
                            }
                        }

                        if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && clickedSquare.equalsT(clickedAndHeldSquare)) {
                            var moves2 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedAndHeldSquare.File && System.Nullable.getValue(x.StartingRank) === clickedAndHeldSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves2.Count === 1) {
                                return moves2.getItem(0);
                            }
                        }

                        if (hasClickedOnNuke && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.equalsT(clickedAndHeldSquare)) {
                            var moves3 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return x.IsNuke && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);
                            if (moves3.Count === 1) {
                                return moves3.getItem(0);
                            }
                        }

                        if (hasClickedAndHeldOnNuke && hoverSquare != null) {
                            var moves4 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return x.IsNuke && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);
                            if (moves4.Count === 1) {
                                return moves4.getItem(0);
                            }
                        }
                    }

                    return null;
                },
                GetClickedSquare: function (mouseInput, previousMouseInput, board, isPlayerWhite, displayProcessing, possibleMoves, clickedSquare, clickedAndHeldSquare, hasNukeAbility, hasUsedNuke, isNukeInFlight) {
                    var hoverSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetHoverSquare(new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput), isPlayerWhite, displayProcessing);

                    if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (hoverSquare != null) {
                            var piece = board.GetPiece(hoverSquare);
                            if (piece !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece) === isPlayerWhite) {
                                if (clickedSquare != null && !clickedSquare.equalsT(hoverSquare)) {
                                    var moves = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                            return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedSquare.File && System.Nullable.getValue(x.StartingRank) === clickedSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                        }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                                    if (moves.Count === 0) {
                                        return null;
                                    }
                                }
                            }
                        }

                        if (hasNukeAbility && !hasUsedNuke && !isNukeInFlight) {
                            if (ChessCompStompWithHacksLibrary.NukeRenderer.IsHoverOverNuke(new ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse(mouseInput))) {
                                return null;
                            }
                        }
                    }

                    if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (clickedSquare == null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.equalsT(clickedAndHeldSquare)) {
                            var piece1 = board.GetPiece(hoverSquare);
                            if (piece1 !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece1) === isPlayerWhite) {
                                return hoverSquare;
                            }
                        }

                        if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.equalsT(clickedAndHeldSquare) && !clickedSquare.equalsT(hoverSquare)) {
                            var moves1 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedSquare.File && System.Nullable.getValue(x.StartingRank) === clickedSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves1.Count === 0) {
                                var piece2 = board.GetPiece(hoverSquare);
                                if (piece2 !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece2) === isPlayerWhite) {
                                    return hoverSquare;
                                }
                            }
                        }

                        return null;
                    }

                    return clickedSquare;
                },
                GetClickedAndHeldSquare: function (mouseInput, previousMouseInput, isPlayerWhite, displayProcessing, clickedAndHeldSquare, isPromotionPanelOpen, promotionPanelX, promotionPanelY) {
                    var hoverSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetHoverSquare(new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput), isPlayerWhite, displayProcessing);

                    if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (hoverSquare != null) {
                            var isHoverOverPanel = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverPanel(promotionPanelX, promotionPanelY, mouseInput);
                            if (!isPromotionPanelOpen || !isHoverOverPanel) {
                                return hoverSquare;
                            }
                        }
                    }

                    if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        return null;
                    }

                    return clickedAndHeldSquare;
                },
                GetPromotionPanelInfo: function (isPromotionPanelOpen, promotionPanelX, promotionPanelY, promotionMoves, mouseInput, previousMouseInput, clickedSquare, clickedAndHeldSquare, isPlayerWhite, displayProcessing, possibleMoves, isNukeInFlight, hasClickedOnNuke) {
                    if (isNukeInFlight || hasClickedOnNuke) {
                        return new ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo(false, 0, 0, null);
                    }

                    var hoverSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetHoverSquare(new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput), isPlayerWhite, displayProcessing);

                    if (isPromotionPanelOpen) {
                        if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                            var isHoverOverPanel = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverPanel(promotionPanelX, promotionPanelY, mouseInput);
                            if (!isHoverOverPanel) {
                                return new ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo(false, 0, 0, null);
                            }
                        }
                    }

                    if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (clickedSquare == null && clickedAndHeldSquare != null && hoverSquare != null && !hoverSquare.equalsT(clickedAndHeldSquare)) {
                            var moves = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedAndHeldSquare.File && System.Nullable.getValue(x.StartingRank) === clickedAndHeldSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves.Count > 0 && moves.getItem(0).Promotion != null) {
                                return new ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo(true, mouseInput.DTLibrary$IMouse$GetX(), mouseInput.DTLibrary$IMouse$GetY(), moves);
                            }
                        } else if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.equalsT(clickedAndHeldSquare) && !clickedSquare.equalsT(hoverSquare)) {
                            var moves1 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedSquare.File && System.Nullable.getValue(x.StartingRank) === clickedSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves1.Count > 0 && moves1.getItem(0).Promotion != null) {
                                return new ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo(true, mouseInput.DTLibrary$IMouse$GetX(), mouseInput.DTLibrary$IMouse$GetY(), moves1);
                            }
                        } else if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && clickedSquare.equalsT(clickedAndHeldSquare)) {
                            var moves2 = System.Linq.Enumerable.from(possibleMoves).where(function (x) {
                                    return !x.IsNuke && System.Nullable.getValue(x.StartingFile) === clickedAndHeldSquare.File && System.Nullable.getValue(x.StartingRank) === clickedAndHeldSquare.Rank && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            if (moves2.Count > 0 && moves2.getItem(0).Promotion != null) {
                                return new ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo(true, mouseInput.DTLibrary$IMouse$GetX(), mouseInput.DTLibrary$IMouse$GetY(), moves2);
                            }
                        }
                    }

                    return new ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo(isPromotionPanelOpen, promotionPanelX, promotionPanelY, promotionMoves);
                },
                GetClickedPromotionPiece: function (isPromotionPanelOpen, promotionPanelX, promotionPanelY, mouseInput, previousMouseInput, clickedPromotionPiece, displayProcessing) {
                    if (isPromotionPanelOpen) {
                        if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                            var hoverOverSquare = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverSquare(promotionPanelX, promotionPanelY, mouseInput, displayProcessing);

                            return hoverOverSquare;
                        }

                        if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                            return null;
                        }

                        return clickedPromotionPiece;
                    }

                    return null;
                },
                GetHasClickedOnNuke: function (board, possibleMoves, isPromotionPanelOpen, mouseInput, previousMouseInput, hasNukeAbility, hasUsedNuke, isNukeInFlight, hasClickedOnNuke, hasClickedAndHeldOnNuke, promotionPanelX, promotionPanelY, isPlayerWhite, displayProcessing) {
                    if (!hasNukeAbility) {
                        return false;
                    }

                    if (hasUsedNuke) {
                        return false;
                    }

                    if (isNukeInFlight) {
                        return false;
                    }

                    if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (isPromotionPanelOpen && ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverPanel(promotionPanelX, promotionPanelY, mouseInput)) {
                            return false;
                        }

                        if (!hasClickedOnNuke && hasClickedAndHeldOnNuke && ChessCompStompWithHacksLibrary.NukeRenderer.IsHoverOverNuke(new ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse(mouseInput))) {
                            return true;
                        }

                        return false;
                    }

                    if (hasClickedOnNuke && mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        var hoverSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetHoverSquare(new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput), isPlayerWhite, displayProcessing);

                        if (hoverSquare != null && !System.Linq.Enumerable.from(possibleMoves).any(function (x) {
                                return x.IsNuke && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                            })) {
                            var piece = board.GetPiece(hoverSquare);
                            if (piece !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece) === isPlayerWhite) {
                                return false;
                            }
                        }
                    }

                    return hasClickedOnNuke;
                },
                GetHasClickedAndHeldOnNuke: function (mouseInput, previousMouseInput, isPromotionPanelOpen, promotionPanelX, promotionPanelY, hasClickedAndHeldOnNuke, hasNukeAbility, hasUsedNuke, isNukeInFlight) {
                    if (!hasNukeAbility) {
                        return false;
                    }

                    if (hasUsedNuke) {
                        return false;
                    }

                    if (isNukeInFlight) {
                        return false;
                    }

                    if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        if (ChessCompStompWithHacksLibrary.NukeRenderer.IsHoverOverNuke(new ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse(mouseInput))) {
                            var isHoverOverPanel = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverPanel(promotionPanelX, promotionPanelY, mouseInput);
                            if (!isPromotionPanelOpen || !isHoverOverPanel) {
                                return true;
                            }
                        }
                    }

                    if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                        return false;
                    }

                    return hasClickedAndHeldOnNuke;
                },
                UpdateChessPiecesRenderer: function (chessPiecesRenderer, gameState, turnCount, isPlayerWhite, isPlayerTurn, clickedSquare, clickedAndHeldSquare, moves, mouseInput, displayProcessing, mostRecentMove, isPromotionPanelOpen, promotionPanelX, promotionPanelY, elapsedMicrosPerFrame, hasClickedOnNuke, hasClickedAndHeldOnNuke, isNukeInFlight) {
                    var $t, $t1;
                    var selectedPiece;
                    if (clickedSquare != null) {
                        selectedPiece = clickedSquare;
                    } else {
                        if (clickedAndHeldSquare != null) {
                            var piece = gameState.Board.GetPiece(clickedAndHeldSquare);
                            if (piece !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece) === isPlayerWhite) {
                                selectedPiece = clickedAndHeldSquare;
                            } else {
                                selectedPiece = null;
                            }
                        } else {
                            selectedPiece = null;
                        }
                    }

                    var possibleMoves = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.ChessSquare)).ctor();
                    if (isPlayerTurn && !isNukeInFlight) {
                        if (clickedSquare != null) {
                            var movesAtThisSquare = System.Linq.Enumerable.from(moves).where(function (m) {
                                    return !m.IsNuke && System.Nullable.getValue(m.StartingFile) === clickedSquare.File && System.Nullable.getValue(m.StartingRank) === clickedSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            $t = Bridge.getEnumerator(movesAtThisSquare);
                            try {
                                while ($t.moveNext()) {
                                    var move = $t.Current;
                                    possibleMoves.add(new ChessCompStompWithHacksEngine.ChessSquare(move.EndingFile, move.EndingRank));
                                }
                            } finally {
                                if (Bridge.is($t, System.IDisposable)) {
                                    $t.System$IDisposable$Dispose();
                                }
                            }
                        } else if (clickedAndHeldSquare != null) {
                            var movesAtThisSquare1 = System.Linq.Enumerable.from(moves).where(function (m) {
                                    return !m.IsNuke && System.Nullable.getValue(m.StartingFile) === clickedAndHeldSquare.File && System.Nullable.getValue(m.StartingRank) === clickedAndHeldSquare.Rank;
                                }).toList(ChessCompStompWithHacksEngine.DisplayMove);

                            $t1 = Bridge.getEnumerator(movesAtThisSquare1);
                            try {
                                while ($t1.moveNext()) {
                                    var move1 = $t1.Current;
                                    possibleMoves.add(new ChessCompStompWithHacksEngine.ChessSquare(move1.EndingFile, move1.EndingRank));
                                }
                            } finally {
                                if (Bridge.is($t1, System.IDisposable)) {
                                    $t1.System$IDisposable$Dispose();
                                }
                            }
                        }
                    }

                    var hoverSquare = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetHoverSquare(new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput), isPlayerWhite, displayProcessing);
                    var isHoverOverPanel = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverPanel(promotionPanelX, promotionPanelY, mouseInput);

                    var hoverPiece = null;
                    if (!hasClickedOnNuke) {
                        if (clickedSquare == null && clickedAndHeldSquare != null) {
                            var piece1 = gameState.Board.GetPiece(clickedAndHeldSquare);
                            if (piece1 !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece1) === isPlayerWhite) {
                                hoverPiece = new ChessCompStompWithHacksLibrary.ChessPiecesRenderer.HoverPieceInfo(piece1, (new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput)).GetX(), (new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput)).GetY());
                            }
                        } else if (clickedSquare != null && clickedAndHeldSquare != null && clickedSquare.equalsT(clickedAndHeldSquare)) {
                            var piece2 = gameState.Board.GetPiece(clickedAndHeldSquare);
                            if (piece2 !== ChessCompStompWithHacksEngine.ChessSquarePiece.Empty && ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece2) === isPlayerWhite) {
                                hoverPiece = new ChessCompStompWithHacksLibrary.ChessPiecesRenderer.HoverPieceInfo(piece2, (new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput)).GetX(), (new ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse(mouseInput)).GetY());
                            }
                        }
                    }

                    var potentialNukeSquaresInfo = null;
                    if ((hasClickedOnNuke || hasClickedAndHeldOnNuke) && hoverSquare != null && turnCount > ChessCompStompWithHacksEngine.TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable && isPlayerTurn) {
                        var isValidSquare = System.Linq.Enumerable.from(moves).any(function (x) {
                                return x.IsNuke && x.EndingFile === hoverSquare.File && x.EndingRank === hoverSquare.Rank;
                            });
                        potentialNukeSquaresInfo = new ChessCompStompWithHacksLibrary.ChessPiecesRenderer.PotentialNukeSquaresInfo(hoverSquare, isValidSquare);
                    }

                    return chessPiecesRenderer.ProcessFrame(gameState.Board, ChessCompStompWithHacksLibrary.ChessPiecesRendererUtil.GetKingInDangerSquare(gameState), mostRecentMove != null ? ChessCompStompWithHacksLibrary.ChessPiecesRendererUtil.GetPreviousMoveSquares$1(mostRecentMove.OriginalGameState, mostRecentMove.Move) : DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare).EmptyList(), selectedPiece, new (DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare)).$ctor1(possibleMoves), potentialNukeSquaresInfo, !isPromotionPanelOpen || !isHoverOverPanel ? hoverSquare : null, hoverPiece, elapsedMicrosPerFrame);
                }
            }
        },
        fields: {
            globalState: null,
            gameState: null,
            moveTracker: null,
            moveTrackerRenderer: null,
            moveTrackerRendererPositionIndex: null,
            chessPiecesRenderer: null,
            promotionPanel: null,
            promotionPanelX: 0,
            promotionPanelY: 0,
            nukeRenderer: null,
            promotionMoves: null,
            clickedSquare: null,
            clickedAndHeldSquare: null,
            isNukeInProgress: false,
            isNukeLiftingOff: false,
            isNukeInFlight: false,
            nukeMove: null,
            hasClickedOnNuke: false,
            hasClickedAndHeldOnNuke: false,
            isPromotionPanelOpen: false,
            chessAI: null,
            gameStatus: 0,
            possibleMoves: null,
            aiElapsedTimeThinking: 0,
            clickedPromotionPiece: null,
            isFinalBattle: false,
            previousMouseInput: null
        },
        ctors: {
            ctor: function (globalState, isPlayerWhite, researchedHacks, aiHackLevel) {
                this.$initialize();
                this.globalState = globalState;
                this.gameState = ChessCompStompWithHacksLibrary.NewGameCreation.CreateNewGame(isPlayerWhite, researchedHacks, aiHackLevel);
                this.moveTracker = new ChessCompStompWithHacksLibrary.MoveTracker();
                this.moveTrackerRenderer = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetMoveTrackerRenderer(this.moveTracker);
                this.moveTrackerRendererPositionIndex = null;

                this.chessPiecesRenderer = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetChessPiecesRenderer(this.gameState.Board, null, DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.ChessSquare).EmptyList(), this.gameState.IsPlayerWhite);
                this.promotionPanel = ChessCompStompWithHacksLibrary.PromotionPanel.GetPromotionPanel(this.gameState.IsPlayerWhite);
                this.promotionPanelX = 0;
                this.promotionPanelY = 0;
                this.nukeRenderer = ChessCompStompWithHacksLibrary.NukeRenderer.GetNukeRenderer(this.gameState.Abilities.HasTacticalNuke, this.gameState.HasUsedNuke, false, null, this.gameState.TurnCount, globalState.Timer);
                this.promotionMoves = null;

                this.clickedSquare = null;
                this.clickedAndHeldSquare = null;

                this.isNukeInProgress = false;
                this.isNukeLiftingOff = false;
                this.isNukeInFlight = false;
                this.nukeMove = null;

                this.hasClickedOnNuke = false;
                this.hasClickedAndHeldOnNuke = false;

                this.isPromotionPanelOpen = false;

                this.chessAI = null;

                var result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(this.gameState);
                this.gameStatus = result.GameStatus;
                this.possibleMoves = ChessCompStompWithHacksEngine.DisplayMove.GetDisplayMoves(result.Moves, this.gameState);

                this.aiElapsedTimeThinking = 0;

                this.clickedPromotionPiece = null;

                this.isFinalBattle = aiHackLevel === ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.FinalBattle;

                this.previousMouseInput = new DTLibrary.EmptyMouse();
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) {
                if (this.chessAI != null) {
                    this.chessAI.ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove(milliseconds);
                }
            },
            ProcessNextFrame: function (mouseInput, displayProcessing, soundOutput, elapsedMicrosPerFrame) {
                var $t, $t1, $t2;
                var previousMouseInput = this.previousMouseInput;
                this.previousMouseInput = new DTLibrary.CopiedMouse(mouseInput);

                if (this.gameStatus !== ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.InProgress) {
                    this.chessPiecesRenderer = ChessCompStompWithHacksLibrary.GameLogic.UpdateChessPiecesRenderer(this.chessPiecesRenderer, this.gameState, this.gameState.TurnCount, this.gameState.IsPlayerWhite, this.gameState.IsPlayerTurn(), null, null, this.possibleMoves, mouseInput, displayProcessing, this.moveTracker.GetMostRecentMove(), this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY, elapsedMicrosPerFrame, false, false, this.isNukeInFlight);

                    this.nukeRenderer = this.nukeRenderer.ProcessFrame(this.gameState.HasUsedNuke, false, null, this.gameState.TurnCount, elapsedMicrosPerFrame);

                    this.moveTrackerRendererPositionIndex = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetHoverOverMove(new ChessCompStompWithHacksLibrary.GameLogic.MoveTrackerRendererMouse(mouseInput));
                    this.moveTrackerRenderer = this.moveTrackerRenderer.ProcessFrame(this.moveTracker, this.moveTrackerRendererPositionIndex, elapsedMicrosPerFrame);

                    return new ChessCompStompWithHacksLibrary.GameLogic.Result(this.gameStatus, new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Objective)).ctor(), this.gameState.IsPlayerWhite, this.isFinalBattle);
                }

                var hasPlayerJustMoved = false;

                var completedObjectives = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Objective)).ctor();

                if (this.isNukeInFlight === false && this.isNukeInProgress) {
                    if (this.chessPiecesRenderer.HasNukeFinished()) {
                        this.isNukeInProgress = false;
                    }
                }

                if (this.isNukeInFlight) {
                    if (this.isNukeLiftingOff && this.nukeRenderer.HasNukeFlownOffScreen()) {
                        this.isNukeLiftingOff = false;
                        this.chessPiecesRenderer = this.chessPiecesRenderer.LandNuke(new ChessCompStompWithHacksEngine.ChessSquare(this.nukeMove.EndingFile, this.nukeMove.EndingRank));
                    }

                    if (this.chessPiecesRenderer.HasNukeLanded()) {
                        this.isNukeInFlight = false;

                        this.moveTracker = this.moveTracker.AddMove(this.gameState, this.nukeMove.Move, this.globalState.Timer);
                        var newlyCompletedObjectives = ChessCompStompWithHacksEngine.ObjectiveChecker.GetCompletedObjectives(this.gameState, this.nukeMove.Move, this.isFinalBattle);
                        $t = Bridge.getEnumerator(newlyCompletedObjectives);
                        try {
                            while ($t.moveNext()) {
                                var objective = $t.Current;
                                completedObjectives.add(objective);
                            }
                        } finally {
                            if (Bridge.is($t, System.IDisposable)) {
                                $t.System$IDisposable$Dispose();
                            }
                        }
                        this.gameState = ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(this.gameState, this.nukeMove.Move);
                        var result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(this.gameState);
                        this.gameStatus = result.GameStatus;
                        this.possibleMoves = ChessCompStompWithHacksEngine.DisplayMove.GetDisplayMoves(result.Moves, this.gameState);
                        this.nukeMove = null;

                        if (this.clickedSquare != null) {
                            var piece = this.gameState.Board.GetPiece(this.clickedSquare);
                            if (piece === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece) !== this.gameState.IsPlayerWhite) {
                                this.clickedSquare = null;
                            }
                        }
                    }
                } else if (!this.gameState.IsPlayerTurn()) {
                    if (!this.isNukeInProgress) {
                        if (this.chessAI == null) {
                            this.chessAI = new ChessCompStompWithHacksEngine.CompositeAI(this.gameState, this.globalState.Timer, this.globalState.Rng, this.globalState.Logger);
                            this.aiElapsedTimeThinking = 0;
                        }

                        this.aiElapsedTimeThinking = (this.aiElapsedTimeThinking + elapsedMicrosPerFrame) | 0;

                        var amountOfTimeElapsedMillis = (Bridge.Int.div(this.aiElapsedTimeThinking, 1000)) | 0;

                        if (!this.chessAI.ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation() && this.chessAI.ChessCompStompWithHacksEngine$IChessAI$GetDepthOfBestMoveFoundSoFar() < 4 && amountOfTimeElapsedMillis > 250) {
                            this.chessAI.ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove(10);
                        }

                        if (this.chessAI.ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation() && amountOfTimeElapsedMillis > 500 || amountOfTimeElapsedMillis > 1500) {
                            var move = this.chessAI.ChessCompStompWithHacksEngine$IChessAI$GetBestMoveFoundSoFar();
                            this.moveTracker = this.moveTracker.AddMove(this.gameState, move, this.globalState.Timer);
                            var newlyCompletedObjectives1 = ChessCompStompWithHacksEngine.ObjectiveChecker.GetCompletedObjectives(this.gameState, move, this.isFinalBattle);
                            $t1 = Bridge.getEnumerator(newlyCompletedObjectives1);
                            try {
                                while ($t1.moveNext()) {
                                    var objective1 = $t1.Current;
                                    completedObjectives.add(objective1);
                                }
                            } finally {
                                if (Bridge.is($t1, System.IDisposable)) {
                                    $t1.System$IDisposable$Dispose();
                                }
                            }
                            this.gameState = ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(this.gameState, move);
                            var result1 = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(this.gameState);
                            this.gameStatus = result1.GameStatus;
                            this.possibleMoves = ChessCompStompWithHacksEngine.DisplayMove.GetDisplayMoves(result1.Moves, this.gameState);

                            if (this.clickedSquare != null) {
                                var piece1 = this.gameState.Board.GetPiece(this.clickedSquare);
                                if (piece1 === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty || ChessCompStompWithHacksEngine.ChessSquarePieceUtil.IsWhite(piece1) !== this.gameState.IsPlayerWhite) {
                                    this.clickedSquare = null;
                                }
                            }

                            this.chessAI = null;
                        }
                    }
                } else {
                    var playerMove = ChessCompStompWithHacksLibrary.GameLogic.GetPlayerMove(mouseInput, previousMouseInput, this.isPromotionPanelOpen, this.clickedPromotionPiece, this.possibleMoves, this.promotionMoves, this.gameState.IsPlayerWhite, this.isNukeInFlight, this.promotionPanelX, this.promotionPanelY, this.clickedSquare, this.clickedAndHeldSquare, this.hasClickedOnNuke, this.hasClickedAndHeldOnNuke, displayProcessing);

                    if (playerMove != null) {
                        if (playerMove.IsNuke) {
                            this.isNukeInProgress = true;
                            this.isNukeInFlight = true;
                            this.isNukeLiftingOff = true;
                            this.nukeMove = playerMove;
                            this.nukeRenderer = this.nukeRenderer.LaunchNuke();
                        } else {
                            this.moveTracker = this.moveTracker.AddMove(this.gameState, playerMove.Move, this.globalState.Timer);
                            var newlyCompletedObjectives2 = ChessCompStompWithHacksEngine.ObjectiveChecker.GetCompletedObjectives(this.gameState, playerMove.Move, this.isFinalBattle);
                            $t2 = Bridge.getEnumerator(newlyCompletedObjectives2);
                            try {
                                while ($t2.moveNext()) {
                                    var objective2 = $t2.Current;
                                    completedObjectives.add(objective2);
                                }
                            } finally {
                                if (Bridge.is($t2, System.IDisposable)) {
                                    $t2.System$IDisposable$Dispose();
                                }
                            }
                            this.gameState = ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove(this.gameState, playerMove);
                            var result2 = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(this.gameState);
                            this.gameStatus = result2.GameStatus;
                            this.possibleMoves = ChessCompStompWithHacksEngine.DisplayMove.GetDisplayMoves(result2.Moves, this.gameState);
                            hasPlayerJustMoved = true;
                            this.clickedSquare = null;
                            this.clickedAndHeldSquare = null;
                            this.isNukeInProgress = false;
                            this.isNukeLiftingOff = false;
                            this.isNukeInFlight = false;
                            this.nukeMove = null;
                            this.hasClickedOnNuke = false;
                            this.hasClickedAndHeldOnNuke = false;
                            this.isPromotionPanelOpen = false;
                            this.clickedPromotionPiece = null;
                            this.promotionMoves = null;
                            this.chessAI = null;
                        }
                    }
                }

                if (!hasPlayerJustMoved) {
                    var newClickedSquare = ChessCompStompWithHacksLibrary.GameLogic.GetClickedSquare(mouseInput, previousMouseInput, this.gameState.Board, this.gameState.IsPlayerWhite, displayProcessing, this.possibleMoves, this.clickedSquare, this.clickedAndHeldSquare, this.gameState.Abilities.HasTacticalNuke, this.gameState.HasUsedNuke, this.isNukeInFlight);

                    var newClickedAndHeldSquare = ChessCompStompWithHacksLibrary.GameLogic.GetClickedAndHeldSquare(mouseInput, previousMouseInput, this.gameState.IsPlayerWhite, displayProcessing, this.clickedAndHeldSquare, this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY);

                    var promotionPanelInfo = ChessCompStompWithHacksLibrary.GameLogic.GetPromotionPanelInfo(this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY, this.promotionMoves, mouseInput, previousMouseInput, this.clickedSquare, this.clickedAndHeldSquare, this.gameState.IsPlayerWhite, displayProcessing, this.possibleMoves, this.isNukeInFlight, this.hasClickedOnNuke);

                    var newClickedPromotionPiece = ChessCompStompWithHacksLibrary.GameLogic.GetClickedPromotionPiece(this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY, mouseInput, previousMouseInput, this.clickedPromotionPiece, displayProcessing);

                    var newHasClickedOnNuke = ChessCompStompWithHacksLibrary.GameLogic.GetHasClickedOnNuke(this.gameState.Board, this.possibleMoves, this.isPromotionPanelOpen, mouseInput, previousMouseInput, this.gameState.Abilities.HasTacticalNuke, this.gameState.HasUsedNuke, this.isNukeInFlight, this.hasClickedOnNuke, this.hasClickedAndHeldOnNuke, this.promotionPanelX, this.promotionPanelY, this.gameState.IsPlayerWhite, displayProcessing);

                    var newHasClickedAndHeldOnNuke = ChessCompStompWithHacksLibrary.GameLogic.GetHasClickedAndHeldOnNuke(mouseInput, previousMouseInput, this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY, this.hasClickedAndHeldOnNuke, this.gameState.Abilities.HasTacticalNuke, this.gameState.HasUsedNuke, this.isNukeInFlight);

                    this.clickedSquare = newClickedSquare;
                    this.clickedAndHeldSquare = newClickedAndHeldSquare;
                    this.isPromotionPanelOpen = promotionPanelInfo.IsPromotionPanelOpen;
                    this.promotionPanelX = promotionPanelInfo.PromotionPanelX;
                    this.promotionPanelY = promotionPanelInfo.PromotionPanelY;
                    this.promotionMoves = promotionPanelInfo.PromotionMoves;
                    this.clickedPromotionPiece = newClickedPromotionPiece;
                    this.hasClickedOnNuke = newHasClickedOnNuke;
                    this.hasClickedAndHeldOnNuke = newHasClickedAndHeldOnNuke;
                }

                this.chessPiecesRenderer = ChessCompStompWithHacksLibrary.GameLogic.UpdateChessPiecesRenderer(this.chessPiecesRenderer, this.gameState, this.gameState.TurnCount, this.gameState.IsPlayerWhite, this.gameState.IsPlayerTurn(), this.clickedSquare, this.clickedAndHeldSquare, this.possibleMoves, mouseInput, displayProcessing, this.moveTracker.GetMostRecentMove(), this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY, elapsedMicrosPerFrame, this.hasClickedOnNuke, this.hasClickedAndHeldOnNuke, this.isNukeInFlight);

                var promotionPanelHoverSquare = ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverSquare(this.promotionPanelX, this.promotionPanelY, mouseInput, displayProcessing);
                this.promotionPanel = this.promotionPanel.ProcessFrame(this.isPromotionPanelOpen, this.promotionPanelX, this.promotionPanelY, promotionPanelHoverSquare, this.clickedPromotionPiece);

                this.nukeRenderer = this.nukeRenderer.ProcessFrame(this.gameState.HasUsedNuke, this.hasClickedOnNuke || this.hasClickedAndHeldOnNuke, ChessCompStompWithHacksLibrary.NukeRenderer.IsHoverOverNuke(new ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse(mouseInput)) ? { Item1: (new ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse(mouseInput)).GetX(), Item2: (new ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse(mouseInput)).GetY() } : null, this.gameState.TurnCount, elapsedMicrosPerFrame);

                var isHoverOverPromotionPanel = this.isPromotionPanelOpen && ChessCompStompWithHacksLibrary.PromotionPanel.IsHoverOverPanel(this.promotionPanelX, this.promotionPanelY, mouseInput);

                this.moveTrackerRendererPositionIndex = isHoverOverPromotionPanel ? null : ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetHoverOverMove(new ChessCompStompWithHacksLibrary.GameLogic.MoveTrackerRendererMouse(mouseInput));
                this.moveTrackerRenderer = this.moveTrackerRenderer.ProcessFrame(this.moveTracker, this.moveTrackerRendererPositionIndex, elapsedMicrosPerFrame);

                return new ChessCompStompWithHacksLibrary.GameLogic.Result(this.gameStatus, new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Objective)).$ctor1(completedObjectives), this.gameState.IsPlayerWhite, this.isFinalBattle);
            },
            Render: function (displayOutput) {
                this.moveTrackerRenderer.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, ChessCompStompWithHacksLibrary.GameLogic.MOVE_TRACKER_RENDERER_X, ChessCompStompWithHacksLibrary.GameLogic.MOVE_TRACKER_RENDERER_Y));

                var moveInfo;

                if (System.Nullable.hasValue(this.moveTrackerRendererPositionIndex)) {
                    moveInfo = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetMoveInfoForHover(System.Nullable.getValue(this.moveTrackerRendererPositionIndex), this.moveTracker);
                } else {
                    moveInfo = null;
                }

                if (moveInfo == null || moveInfo.NewGameState.TurnCount === this.gameState.TurnCount) {
                    this.chessPiecesRenderer.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, ChessCompStompWithHacksLibrary.GameLogic.CHESS_PIECES_RENDERER_X, ChessCompStompWithHacksLibrary.GameLogic.CHESS_PIECES_RENDERER_Y));
                    this.nukeRenderer.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, ChessCompStompWithHacksLibrary.GameLogic.NUKE_RENDERER_X, ChessCompStompWithHacksLibrary.GameLogic.NUKE_RENDERER_Y));
                    this.promotionPanel.Render(displayOutput); // must render after moveTrackerRenderer to ensure panel is on top
                } else {
                    moveInfo.NewStateChessPiecesRenderer.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, ChessCompStompWithHacksLibrary.GameLogic.CHESS_PIECES_RENDERER_X, ChessCompStompWithHacksLibrary.GameLogic.CHESS_PIECES_RENDERER_Y));
                    moveInfo.NewStateNukeRenderer.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, ChessCompStompWithHacksLibrary.GameLogic.NUKE_RENDERER_X, ChessCompStompWithHacksLibrary.GameLogic.NUKE_RENDERER_Y));
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GameLogic.PromotionPanelInfo", {
        $kind: "nested class",
        fields: {
            IsPromotionPanelOpen: false,
            PromotionPanelX: 0,
            PromotionPanelY: 0,
            PromotionMoves: null
        },
        ctors: {
            ctor: function (isPromotionPanelOpen, promotionPanelX, promotionPanelY, promotionMoves) {
                this.$initialize();
                this.IsPromotionPanelOpen = isPromotionPanelOpen;
                this.PromotionPanelX = promotionPanelX;
                this.PromotionPanelY = promotionPanelY;
                this.PromotionMoves = promotionMoves;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GameLogic.Result", {
        $kind: "nested class",
        fields: {
            GameStatus: 0,
            CompletedObjectives: null,
            IsPlayerWhite: false,
            IsFinalBattle: false
        },
        ctors: {
            ctor: function (gameStatus, completedObjectives, isPlayerWhite, isFinalBattle) {
                this.$initialize();
                this.GameStatus = gameStatus;
                this.CompletedObjectives = completedObjectives;
                this.IsPlayerWhite = isPlayerWhite;
                this.IsFinalBattle = isFinalBattle;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GlobalState", {
        statics: {
            fields: {
                DEFAULT_VOLUME: 0
            },
            ctors: {
                init: function () {
                    this.DEFAULT_VOLUME = 50;
                }
            }
        },
        fields: {
            Fps: 0,
            Rng: null,
            GuidGenerator: null,
            Logger: null,
            Timer: null,
            IsWebBrowserVersion: false,
            DebugMode: false,
            ShowSoundAndMusicVolumePicker: false,
            desiredMusicVolume: 0,
            currentMusicVolume: 0,
            MusicPlayer: null,
            ElapsedMicrosPerFrame: 0
        },
        props: {
            MusicVolume: {
                get: function () {
                    return this.desiredMusicVolume;
                },
                set: function (value) {
                    this.desiredMusicVolume = value;
                }
            }
        },
        ctors: {
            ctor: function (fps, rng, guidGenerator, logger, timer, isWebBrowserVersion, debugMode, initialMusicVolume, showSoundAndMusicVolumePicker) {
                var $t;
                this.$initialize();
                this.Fps = fps;
                this.Rng = rng;
                this.GuidGenerator = guidGenerator;
                this.Logger = logger;
                this.Timer = timer;
                this.IsWebBrowserVersion = isWebBrowserVersion;
                this.DebugMode = debugMode;
                this.desiredMusicVolume = ($t = initialMusicVolume, $t != null ? $t : ChessCompStompWithHacksLibrary.GlobalState.DEFAULT_VOLUME);
                this.currentMusicVolume = this.desiredMusicVolume;

                var elapsedMicrosPerFrame = (Bridge.Int.div(1000000, fps)) | 0;

                this.MusicPlayer = new ChessCompStompWithHacksLibrary.MusicPlayer(elapsedMicrosPerFrame);
                this.ElapsedMicrosPerFrame = elapsedMicrosPerFrame;

                this.ShowSoundAndMusicVolumePicker = showSoundAndMusicVolumePicker;
            }
        },
        methods: {
            ProcessMusic: function () {
                this.MusicPlayer.ProcessFrame();
                this.currentMusicVolume = DTLibrary.VolumeUtil.GetVolumeSmoothed(this.ElapsedMicrosPerFrame, this.currentMusicVolume, this.desiredMusicVolume);
            },
            RenderMusic: function (musicOutput) {
                this.MusicPlayer.RenderMusic(musicOutput, this.currentMusicVolume);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.HackDisplay", {
        statics: {
            fields: {
                WIDTH: 0,
                HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.WIDTH = 190;
                    this.HEIGHT = 100;
                }
            }
        },
        fields: {
            hack: 0,
            x: 0,
            y: 0,
            sessionState: null,
            isHover: false,
            isClick: false,
            mouseX: 0,
            mouseY: 0
        },
        ctors: {
            ctor: function (hack, x, y, sessionState) {
                this.$initialize();
                this.hack = hack;
                this.x = x;
                this.y = y;
                this.sessionState = sessionState;

                this.mouseX = 0;
                this.mouseY = 0;

                this.isHover = false;
                this.isClick = false;
            }
        },
        methods: {
            ProcessFrame: function (mouseInput, previousMouseInput, displayProcessing) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                this.mouseX = mouseX;
                this.mouseY = mouseY;

                var isHover = this.x <= mouseX && mouseX <= ((this.x + ChessCompStompWithHacksLibrary.HackDisplay.WIDTH) | 0) && this.y <= mouseY && mouseY <= ((this.y + ChessCompStompWithHacksLibrary.HackDisplay.HEIGHT) | 0);

                this.isHover = isHover;
                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && isHover) {
                    this.isClick = true;
                }

                if (this.isClick && !mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.isClick = false;

                    if (isHover && this.CanAffordHack()) {
                        this.sessionState.AddResearchedHack(this.hack);
                    }
                }
            },
            CanAffordHack: function () {
                return this.sessionState.GetUnusedHackPoints() >= ChessCompStompWithHacksLibrary.HackUtil.GetHackCost(this.hack);
            },
            RenderHoverDisplay: function (displayOutput) {
                if (this.isHover) {
                    var hackDescription = ChessCompStompWithHacksLibrary.HackUtil.GetHackDescriptionForHackSelectionScreen(this.hack);
                    var text = hackDescription.Description;

                    var numberOfNewLines = 0;
                    for (var i = 0; i < text.length; i = (i + 1) | 0) {
                        if (text.charCodeAt(i) === 10) {
                            numberOfNewLines = (numberOfNewLines + 1) | 0;
                        }
                    }

                    var width = hackDescription.Width;
                    var height = (Bridge.Int.mul(19, (((numberOfNewLines + 1) | 0))) + 20) | 0;

                    var x;
                    if (((this.mouseX + width) | 0) > ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH) {
                        x = (this.mouseX - width) | 0;
                    } else {
                        x = this.mouseX;
                    }

                    var y;
                    if (((this.mouseY - height) | 0) < 0) {
                        y = this.mouseY;
                    } else {
                        y = (this.mouseY - height) | 0;
                    }

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(x, y, ((width - 1) | 0), ((height - 1) | 0), new DTLibrary.DTColor.ctor(255, 245, 171), true);
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(x, y, width, height, DTLibrary.DTColor.Black(), false);

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 25) | 0), ((((y + height) | 0) - 10) | 0), text, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, DTLibrary.DTColor.Black());
                }
            },
            RenderButtonDisplay: function (displayOutput) {
                var backgroundColor;

                var hasResearchedHack = this.sessionState.GetResearchedHacks().contains(this.hack);
                var canAffordHack = this.CanAffordHack();

                if (hasResearchedHack) {
                    backgroundColor = new DTLibrary.DTColor.ctor(201, 255, 196);
                } else {
                    if (canAffordHack && this.isClick) {
                        backgroundColor = new DTLibrary.DTColor.ctor(252, 251, 154);
                    } else {
                        if (canAffordHack && this.isHover) {
                            backgroundColor = new DTLibrary.DTColor.ctor(250, 249, 200);
                        } else {
                            if (canAffordHack) {
                                backgroundColor = new DTLibrary.DTColor.ctor(235, 235, 235);
                            } else {
                                backgroundColor = new DTLibrary.DTColor.ctor(200, 200, 200);
                            }
                        }
                    }
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, 189, 99, backgroundColor, true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, ChessCompStompWithHacksLibrary.HackDisplay.WIDTH, ChessCompStompWithHacksLibrary.HackDisplay.HEIGHT, DTLibrary.DTColor.Black(), false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + 10) | 0), ((this.y + 90) | 0), ChessCompStompWithHacksLibrary.HackUtil.GetHackNameForHackSelectionScreen(this.hack), ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + 10) | 0), ((this.y + 39) | 0), "Cost: " + (DTLibrary.StringUtil.ToStringCultureInvariant(ChessCompStompWithHacksLibrary.HackUtil.GetHackCost(this.hack)) || "") + " hack points", ChessCompStompWithHacksLibrary.ChessFont.Fetamont12Pt, new DTLibrary.DTColor.ctor(128, 128, 128));

                if (hasResearchedHack) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + 10) | 0), ((this.y + 20) | 0), "Hack implemented", ChessCompStompWithHacksLibrary.ChessFont.Fetamont12Pt, new DTLibrary.DTColor.ctor(128, 128, 128));
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.HackUtil", {
        statics: {
            methods: {
                GetHackCost: function (hack) {
                    switch (hack) {
                        case ChessCompStompWithHacksEngine.Hack.ExtraPawnFirst: 
                        case ChessCompStompWithHacksEngine.Hack.ExtraPawnSecond: 
                        case ChessCompStompWithHacksEngine.Hack.StalemateIsVictory: 
                        case ChessCompStompWithHacksEngine.Hack.SuperCastling: 
                        case ChessCompStompWithHacksEngine.Hack.PawnsCanMoveThreeSpacesInitially: 
                            return 3;
                        case ChessCompStompWithHacksEngine.Hack.RooksCanMoveLikeBishops: 
                        case ChessCompStompWithHacksEngine.Hack.ExtraQueen: 
                        case ChessCompStompWithHacksEngine.Hack.RooksCanCaptureLikeCannons: 
                        case ChessCompStompWithHacksEngine.Hack.KnightsCanMakeLargeKnightsMove: 
                        case ChessCompStompWithHacksEngine.Hack.QueensCanMoveLikeKnights: 
                            return 5;
                        case ChessCompStompWithHacksEngine.Hack.SuperEnPassant: 
                        case ChessCompStompWithHacksEngine.Hack.AnyPieceCanPromote: 
                        case ChessCompStompWithHacksEngine.Hack.OpponentMustCaptureWhenPossible: 
                            return 10;
                        case ChessCompStompWithHacksEngine.Hack.PawnsDestroyCapturingPiece: 
                        case ChessCompStompWithHacksEngine.Hack.TacticalNuke: 
                            return 20;
                        default: 
                            throw new System.Exception();
                    }
                },
                GetHackNameForHackSelectionScreen: function (hack) {
                    switch (hack) {
                        case ChessCompStompWithHacksEngine.Hack.ExtraPawnFirst: 
                            return "Extra pawn";
                        case ChessCompStompWithHacksEngine.Hack.ExtraPawnSecond: 
                            return "Another extra\npawn";
                        case ChessCompStompWithHacksEngine.Hack.ExtraQueen: 
                            return "Extra queen";
                        case ChessCompStompWithHacksEngine.Hack.PawnsCanMoveThreeSpacesInitially: 
                            return "Pawn boost";
                        case ChessCompStompWithHacksEngine.Hack.SuperEnPassant: 
                            return "Super\nen passant";
                        case ChessCompStompWithHacksEngine.Hack.RooksCanMoveLikeBishops: 
                            return "Diagonal rooks";
                        case ChessCompStompWithHacksEngine.Hack.SuperCastling: 
                            return "Super castling";
                        case ChessCompStompWithHacksEngine.Hack.RooksCanCaptureLikeCannons: 
                            return "Cannoning";
                        case ChessCompStompWithHacksEngine.Hack.KnightsCanMakeLargeKnightsMove: 
                            return "Upgraded\nknights";
                        case ChessCompStompWithHacksEngine.Hack.QueensCanMoveLikeKnights: 
                            return "Upgraded queen";
                        case ChessCompStompWithHacksEngine.Hack.TacticalNuke: 
                            return "Tactical nuke";
                        case ChessCompStompWithHacksEngine.Hack.AnyPieceCanPromote: 
                            return "Equitable\npromotions";
                        case ChessCompStompWithHacksEngine.Hack.StalemateIsVictory: 
                            return "Anti-stalemate";
                        case ChessCompStompWithHacksEngine.Hack.OpponentMustCaptureWhenPossible: 
                            return "Mandatory\ncaptures";
                        case ChessCompStompWithHacksEngine.Hack.PawnsDestroyCapturingPiece: 
                            return "Sacrificial\npawns";
                        default: 
                            throw new System.Exception();
                    }
                },
                GetHackDescriptionForHackSelectionScreen: function (hack) {
                    switch (hack) {
                        case ChessCompStompWithHacksEngine.Hack.ExtraPawnFirst: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Start with an extra pawn.", 301);
                        case ChessCompStompWithHacksEngine.Hack.ExtraPawnSecond: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Start with another extra pawn.", 357);
                        case ChessCompStompWithHacksEngine.Hack.ExtraQueen: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Start with an extra queen.", 309);
                        case ChessCompStompWithHacksEngine.Hack.PawnsCanMoveThreeSpacesInitially: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("The first time a pawn moves, the pawn may\nmove forward 3 squares.", 460);
                        case ChessCompStompWithHacksEngine.Hack.SuperEnPassant: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Your pawns may capture enemy pieces that are\nhorizontally adjacent to the pawn.\nSuper en passant is allowed regardless of\nwhen or how the enemy piece moved.\nThe pawn may capture super en passant\nregardless of which rank the pawn is on.", 500);
                        case ChessCompStompWithHacksEngine.Hack.RooksCanMoveLikeBishops: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("In addition to their normal moves, your rooks\nmay also move as if they were bishops.", 489);
                        case ChessCompStompWithHacksEngine.Hack.SuperCastling: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("You may castle as long as there are no pieces\nbetween your king and rook.\nSuper castling is allowed regardless of\nwhether the king or rook has previously moved.\nYou cannot super castle out of, through,\nor into check.\nSuper castling is allowed both horizontally\nand vertically.", 500);
                        case ChessCompStompWithHacksEngine.Hack.RooksCanCaptureLikeCannons: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Your rooks may capture enemy pieces even if\nthere is a piece between your rook and the\npiece being captured.", 486);
                        case ChessCompStompWithHacksEngine.Hack.KnightsCanMakeLargeKnightsMove: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Your knights may make large knight's moves\n(moving forward 3 squares and 1 square to\nthe side).", 472);
                        case ChessCompStompWithHacksEngine.Hack.QueensCanMoveLikeKnights: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Your queen may also move as\nif it were a knight.", 336);
                        case ChessCompStompWithHacksEngine.Hack.TacticalNuke: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("You start each game with a nuke.\nThe nuke requires " + (DTLibrary.StringUtil.ToStringCultureInvariant(ChessCompStompWithHacksEngine.TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable) || "") + " turns before" + "\n" + "it is operational.", 380);
                        case ChessCompStompWithHacksEngine.Hack.AnyPieceCanPromote: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Your rooks, knights, bishops, and queen may\npromote upon reaching the last rank.", 476);
                        case ChessCompStompWithHacksEngine.Hack.StalemateIsVictory: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("If it is your turn and you have no legal\nmoves, you win the game.\nIf it is your opponent's turn and your\nopponent has no legal moves, you win\nthe game.", 428);
                        case ChessCompStompWithHacksEngine.Hack.OpponentMustCaptureWhenPossible: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("Capturing is compulsory for your opponent\n(if your opponent can capture a piece, your\nopponent must capture a piece).", 476);
                        case ChessCompStompWithHacksEngine.Hack.PawnsDestroyCapturingPiece: 
                            return new ChessCompStompWithHacksLibrary.HackUtil.HackDescription("When any of your pawns are captured,\nthe capturing piece is also removed\nfrom the board.", 428);
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.HackUtil.HackDescription", {
        $kind: "nested class",
        fields: {
            Description: null,
            Width: 0
        },
        ctors: {
            ctor: function (description, width) {
                this.$initialize();
                this.Description = description;
                this.Width = width;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MoveTracker", {
        statics: {
            fields: {
                MaxNumberOfMovesTracked: 0
            },
            ctors: {
                init: function () {
                    this.MaxNumberOfMovesTracked = 50;
                }
            }
        },
        fields: {
            moves: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                this.moves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo)).ctor();
            }
        },
        methods: {
            AddMove: function (originalGameState, move, timer) {
                var newGameState = ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(originalGameState, move);

                var newTracker = new ChessCompStompWithHacksLibrary.MoveTracker();

                newTracker.moves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo)).$ctor1(this.moves);

                var newGameStateChessPiecesRenderer = ChessCompStompWithHacksLibrary.ChessPiecesRenderer.GetChessPiecesRenderer(newGameState.Board, ChessCompStompWithHacksLibrary.ChessPiecesRendererUtil.GetKingInDangerSquare(newGameState), ChessCompStompWithHacksLibrary.ChessPiecesRendererUtil.GetPreviousMoveSquares$1(originalGameState, move), newGameState.IsPlayerWhite);

                var newGameStateNukeRenderer = ChessCompStompWithHacksLibrary.NukeRenderer.GetNukeRenderer(newGameState.Abilities.HasTacticalNuke, newGameState.HasUsedNuke, false, null, newGameState.TurnCount, timer);

                newTracker.moves.add(new ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo(originalGameState, newGameState, newGameStateChessPiecesRenderer, newGameStateNukeRenderer, move));

                if (newTracker.moves.Count > ChessCompStompWithHacksLibrary.MoveTracker.MaxNumberOfMovesTracked) {
                    newTracker.moves.removeAt(0);
                }

                return newTracker;
            },
            GetMostRecentMove: function () {
                if (this.moves.Count === 0) {
                    return null;
                }
                return this.moves.getItem(((this.moves.Count - 1) | 0));
            },
            GetRecentMoves: function () {
                return new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo)).$ctor1(this.moves);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo", {
        $kind: "nested class",
        fields: {
            OriginalGameState: null,
            NewGameState: null,
            NewStateChessPiecesRenderer: null,
            NewStateNukeRenderer: null,
            Move: null,
            MoveName: null
        },
        ctors: {
            ctor: function (originalGameState, newGameState, newGameStateChessPiecesRenderer, newGameStateNukeRenderer, move) {
                this.$initialize();
                this.OriginalGameState = originalGameState;
                this.NewGameState = newGameState;
                this.NewStateChessPiecesRenderer = newGameStateChessPiecesRenderer;
                this.NewStateNukeRenderer = newGameStateNukeRenderer;
                this.Move = move;
                this.MoveName = ChessCompStompWithHacksEngine.MoveNaming.GetNameOfMove(move, originalGameState);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MoveTrackerRenderer", {
        statics: {
            fields: {
                HOVER_HIGHLIGHT_DURATION_MICROS: 0
            },
            ctors: {
                init: function () {
                    this.HOVER_HIGHLIGHT_DURATION_MICROS = 1000000;
                }
            },
            methods: {
                GetMoveTrackerRenderer: function (moveTracker) {
                    return new ChessCompStompWithHacksLibrary.MoveTrackerRenderer(moveTracker, new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HoverInfo)).ctor());
                },
                /**
                 * Returns the positionIndex of the move the mouse is hovering over.
                 Returns null if the mouse isn't hovering over the MoveTrackerRenderer.
                 *
                 * @static
                 * @public
                 * @this ChessCompStompWithHacksLibrary.MoveTrackerRenderer
                 * @memberof ChessCompStompWithHacksLibrary.MoveTrackerRenderer
                 * @param   {DTLibrary.IMouse}    mouseInput
                 * @return  {?number}
                 */
                GetHoverOverMove: function (mouseInput) {
                    var $t;
                    var moveDisplays = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetMoveDisplays();

                    var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                    var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                    $t = Bridge.getEnumerator(moveDisplays);
                    try {
                        while ($t.moveNext()) {
                            var moveDisplay = $t.Current;
                            if (moveDisplay.X <= mouseX && mouseX <= ((moveDisplay.X + ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay.WIDTH) | 0) && moveDisplay.Y <= mouseY && mouseY <= ((moveDisplay.Y + ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay.HEIGHT) | 0)) {
                                return moveDisplay.PositionIndex;
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    return null;
                },
                GetMoveInfoForHover: function (positionIndex, moveTracker) {
                    var $t;
                    var moveDisplayMapping = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetMoveDisplayMapping(moveTracker);

                    $t = Bridge.getEnumerator(moveDisplayMapping);
                    try {
                        while ($t.moveNext()) {
                            var x = $t.Current;
                            var moveDisplay = x.Item1;
                            var moveInfo = x.Item2;

                            if (moveDisplay.PositionIndex === positionIndex) {
                                return moveInfo;
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    return null;
                },
                GetMoveDisplays: function () {
                    var list = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay)).ctor();

                    var isWhite = true;
                    var y = 306;

                    for (var i = 0; i < 20; i = (i + 1) | 0) {
                        if (isWhite) {
                            list.add(new ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay(i, 0, y));
                        } else {
                            list.add(new ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay(i, 124, y));
                            y = (y - (34)) | 0;
                        }

                        isWhite = !isWhite;
                    }

                    return list;
                },
                GetMoveDisplayMapping: function (moveTracker) {
                    var moveInfos = moveTracker.GetRecentMoves();

                    if (moveInfos.Count === 0) {
                        return new (System.Collections.Generic.List$1(System.Tuple$2(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay,ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo))).ctor();
                    }

                    var index = moveInfos.Count;
                    index = (index - 20) | 0; // show last 20 moves
                    if (index < 0) {
                        index = 0;
                    }

                    if (!moveInfos.getItem(index).OriginalGameState.IsWhiteTurn) {
                        index = (index + 1) | 0;
                        if (index === moveInfos.Count) {
                            return new (System.Collections.Generic.List$1(System.Tuple$2(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay,ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo))).ctor();
                        }
                    }

                    var moveDisplays = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetMoveDisplays();
                    var moveDisplayIndex = 0;

                    var returnValue = new (System.Collections.Generic.List$1(System.Tuple$2(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay,ChessCompStompWithHacksLibrary.MoveTracker.MoveInfo))).ctor();

                    while (true) {
                        if (index === moveInfos.Count) {
                            if (moveInfos.getItem(((moveInfos.Count - 1) | 0)).OriginalGameState.IsWhiteTurn) {
                                returnValue.add({ Item1: moveDisplays.getItem(moveDisplayIndex), Item2: null });
                            }
                            return returnValue;
                        }

                        var moveDisplay = moveDisplays.getItem(moveDisplayIndex);

                        returnValue.add({ Item1: moveDisplay, Item2: moveInfos.getItem(index) });

                        moveDisplayIndex = (moveDisplayIndex + 1) | 0;
                        index = (index + 1) | 0;
                    }
                }
            }
        },
        fields: {
            moveTracker: null,
            hoverInfos: null
        },
        ctors: {
            ctor: function (moveTracker, hoverInfos) {
                this.$initialize();
                this.moveTracker = moveTracker;
                this.hoverInfos = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HoverInfo)).$ctor1(hoverInfos);
            }
        },
        methods: {
            ProcessFrame: function (moveTracker, hoverPositionIndex, elapsedMicrosPerFrame) {
                var $t;
                var newHoverInfos = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HoverInfo)).ctor();

                $t = Bridge.getEnumerator(this.hoverInfos);
                try {
                    while ($t.moveNext()) {
                        var hoverInfo = $t.Current;
                        var positionIndex = hoverInfo.PositionIndex;
                        var elapsedMicros = (hoverInfo.ElapsedMicros + elapsedMicrosPerFrame) | 0;

                        if (elapsedMicros <= ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HOVER_HIGHLIGHT_DURATION_MICROS && (hoverPositionIndex == null || System.Nullable.getValue(hoverPositionIndex) !== positionIndex)) {
                            newHoverInfos.add(new ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HoverInfo(positionIndex, elapsedMicros));
                        }
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                if (hoverPositionIndex != null) {
                    newHoverInfos.add(new ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HoverInfo(System.Nullable.getValue(hoverPositionIndex), 0));
                }

                return new ChessCompStompWithHacksLibrary.MoveTrackerRenderer(moveTracker, newHoverInfos);
            },
            Render: function (displayOutput) {
                var $t, $t1;
                var moveDisplayMapping = ChessCompStompWithHacksLibrary.MoveTrackerRenderer.GetMoveDisplayMapping(this.moveTracker);

                var hoverInfoMapping = new (System.Collections.Generic.Dictionary$2(System.Int32,System.Int32))();
                $t = Bridge.getEnumerator(this.hoverInfos);
                try {
                    while ($t.moveNext()) {
                        var hoverInfo = $t.Current;
                        hoverInfoMapping.set(hoverInfo.PositionIndex, hoverInfo.ElapsedMicros);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                $t1 = Bridge.getEnumerator(moveDisplayMapping);
                try {
                    while ($t1.moveNext()) {
                        var entry = $t1.Current;
                        var moveDisplay = entry.Item1;
                        var moveInfo = entry.Item2;
                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(moveDisplay.X, moveDisplay.Y, 124, 34, new DTLibrary.DTColor.ctor(255, 245, 171), true);
                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(moveDisplay.X, moveDisplay.Y, ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay.WIDTH, ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay.HEIGHT, DTLibrary.DTColor.Black(), false);

                        if (hoverInfoMapping.containsKey(moveDisplay.PositionIndex)) {
                            var elapsedMicros = hoverInfoMapping.get(moveDisplay.PositionIndex);
                            var alpha = (Bridge.Int.div(Bridge.Int.mul((((ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HOVER_HIGHLIGHT_DURATION_MICROS - elapsedMicros) | 0)), 150), ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HOVER_HIGHLIGHT_DURATION_MICROS)) | 0;
                            if (alpha > 255) {
                                alpha = 255;
                            }
                            if (alpha < 0) {
                                alpha = 0;
                            }
                            var fadeColor = new DTLibrary.DTColor.$ctor1(255, 255, 255, alpha);

                            displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(moveDisplay.X, moveDisplay.Y, 124, 34, fadeColor, true);
                        }

                        if (moveInfo != null) {
                            displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((moveDisplay.X + 2) | 0), ((moveDisplay.Y + 28) | 0), moveInfo.MoveName, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, DTLibrary.DTColor.Black());
                        }
                    }
                } finally {
                    if (Bridge.is($t1, System.IDisposable)) {
                        $t1.System$IDisposable$Dispose();
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MoveTrackerRenderer.HoverInfo", {
        $kind: "nested class",
        fields: {
            PositionIndex: 0,
            ElapsedMicros: 0
        },
        ctors: {
            ctor: function (positionIndex, elapsedMicros) {
                this.$initialize();
                this.PositionIndex = positionIndex;
                this.ElapsedMicros = elapsedMicros;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay", {
        $kind: "nested class",
        statics: {
            fields: {
                WIDTH: 0,
                HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.WIDTH = 125;
                    this.HEIGHT = 35;
                }
            }
        },
        fields: {
            /**
             * 0 means the first row, first col
             1 means the first rol, second col
             2 means the second row, first col
             3 means the second row, second col
             etc
             *
             * @instance
             * @public
             * @memberof ChessCompStompWithHacksLibrary.MoveTrackerRenderer.MoveDisplay
             * @function PositionIndex
             * @type number
             */
            PositionIndex: 0,
            X: 0,
            Y: 0
        },
        ctors: {
            ctor: function (positionIndex, x, y) {
                this.$initialize();
                this.PositionIndex = positionIndex;
                this.X = x;
                this.Y = y;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MusicPlayer", {
        fields: {
            /**
             * The current music being played, or null if no music is playing.
             This may not be the same as intendedMusic since it takes a while
             to fade out an existing music and fade in a new one
             *
             * @instance
             * @private
             * @memberof ChessCompStompWithHacksLibrary.MusicPlayer
             * @type ?ChessCompStompWithHacksLibrary.ChessMusic
             */
            currentMusic: null,
            /**
             * The intended music that should eventually play, or null if we should fade out all music
             *
             * @instance
             * @private
             * @memberof ChessCompStompWithHacksLibrary.MusicPlayer
             * @type ?ChessCompStompWithHacksLibrary.ChessMusic
             */
            intendedMusic: null,
            /**
             * From 0 to 100 * 1024 (both inclusive)
             Normally, this value is 100 * 1024.
             However, when fading in/out, this value will decrease to represent the drop in music volume.
             *
             * @instance
             * @private
             * @memberof ChessCompStompWithHacksLibrary.MusicPlayer
             * @type number
             */
            currentFadeInAndOutVolumeMillis: 0,
            /**
             * From 0 to 100.
             For this.currentMusic, the intended volume at which the music should be played.
             We allow this to be set since we might want to play a particular music at a different
             volume depending on circumstances (e.g. maybe the music should be played softer when
             the game is paused)
             *
             * @instance
             * @private
             * @memberof ChessCompStompWithHacksLibrary.MusicPlayer
             * @type number
             */
            currentMusicVolume: 0,
            /**
             * From 0 to 100.
             For this.intendedMusic, the intended volume at which the music should be played.
             *
             * @instance
             * @private
             * @memberof ChessCompStompWithHacksLibrary.MusicPlayer
             * @type number
             */
            intendedMusicVolume: 0,
            elapsedMicrosPerFrame: 0
        },
        ctors: {
            ctor: function (elapsedMicrosPerFrame) {
                this.$initialize();
                this.currentMusic = null;
                this.intendedMusic = null;
                this.currentFadeInAndOutVolumeMillis = 0;
                this.currentMusicVolume = 0;
                this.intendedMusicVolume = 0;

                this.elapsedMicrosPerFrame = elapsedMicrosPerFrame;
            }
        },
        methods: {
            DecreaseCurrentFadeInAndOutVolumeMillis: function () {
                this.currentFadeInAndOutVolumeMillis = (this.currentFadeInAndOutVolumeMillis - ((Bridge.Int.div(this.elapsedMicrosPerFrame, 10)) | 0)) | 0;
                if (this.currentFadeInAndOutVolumeMillis < 0) {
                    this.currentFadeInAndOutVolumeMillis = 0;
                }
            },
            IncreaseCurrentFadeInAndOutVolumeMillis: function () {
                this.currentFadeInAndOutVolumeMillis = (this.currentFadeInAndOutVolumeMillis + ((Bridge.Int.div(this.elapsedMicrosPerFrame, 10)) | 0)) | 0;
                if (this.currentFadeInAndOutVolumeMillis > 102400) {
                    this.currentFadeInAndOutVolumeMillis = 102400;
                }
            },
            ProcessFrame: function () {
                if (this.intendedMusic == null) {
                    if (this.currentMusic != null) {
                        this.DecreaseCurrentFadeInAndOutVolumeMillis();
                        if (this.currentFadeInAndOutVolumeMillis === 0) {
                            this.currentMusic = null;
                        }
                    }

                    return;
                }

                if (this.currentMusic == null) {
                    this.currentMusic = this.intendedMusic;
                    this.currentFadeInAndOutVolumeMillis = 0;
                    this.currentMusicVolume = this.intendedMusicVolume;
                    return;
                }

                if (System.Nullable.getValue(this.currentMusic) !== System.Nullable.getValue(this.intendedMusic)) {
                    this.DecreaseCurrentFadeInAndOutVolumeMillis();
                    if (this.currentFadeInAndOutVolumeMillis === 0) {
                        this.currentMusic = null;
                    }
                    return;
                }

                if (this.currentMusicVolume < this.intendedMusicVolume) {
                    var delta = (Bridge.Int.div(this.elapsedMicrosPerFrame, 5000)) | 0;
                    if (delta === 0) {
                        delta = 1;
                    }
                    this.currentMusicVolume = (this.currentMusicVolume + delta) | 0;
                    if (this.currentMusicVolume > this.intendedMusicVolume) {
                        this.currentMusicVolume = this.intendedMusicVolume;
                    }
                }

                if (this.currentMusicVolume > this.intendedMusicVolume) {
                    var delta1 = (Bridge.Int.div(this.elapsedMicrosPerFrame, 5000)) | 0;
                    if (delta1 === 0) {
                        delta1 = 1;
                    }
                    this.currentMusicVolume = (this.currentMusicVolume - delta1) | 0;
                    if (this.currentMusicVolume < this.intendedMusicVolume) {
                        this.currentMusicVolume = this.intendedMusicVolume;
                    }
                }

                this.IncreaseCurrentFadeInAndOutVolumeMillis();
            },
            SetMusic: function (music, volume) {
                this.intendedMusic = music;
                this.intendedMusicVolume = volume;
            },
            StopMusic: function () {
                this.intendedMusic = null;
            },
            RenderMusic: function (musicOutput, userVolume) {
                if (this.currentMusic != null) {
                    musicOutput.DTLibrary$IMusicOutput$1$ChessCompStompWithHacksLibrary$ChessMusic$PlayMusic(System.Nullable.getValue(this.currentMusic), ((Bridge.Int.div(Bridge.Int.mul(((((Bridge.Int.div(Bridge.Int.mul(this.currentFadeInAndOutVolumeMillis, this.currentMusicVolume), 100)) | 0)) >> 10), userVolume), 100)) | 0));
                } else {
                    musicOutput.DTLibrary$IMusicOutput$1$ChessCompStompWithHacksLibrary$ChessMusic$StopMusic();
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.MusicVolumePicker", {
        fields: {
            _xPos: 0,
            _yPos: 0,
            _currentVolume: 0,
            _unmuteVolume: 0,
            _isDraggingVolumeSlider: false
        },
        ctors: {
            ctor: function (xPos, yPos, initialVolume) {
                this.$initialize();
                this._xPos = xPos;
                this._yPos = yPos;

                this._currentVolume = initialVolume;
                this._unmuteVolume = this._currentVolume;

                this._isDraggingVolumeSlider = false;
            }
        },
        methods: {
            ProcessFrame: function (mouseInput, previousMouseInput) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && this._xPos <= mouseX && mouseX <= ((this._xPos + 40) | 0) && this._yPos <= mouseY && mouseY <= ((this._yPos + 50) | 0)) {
                    if (this._currentVolume === 0) {
                        this._currentVolume = this._unmuteVolume === 0 ? ChessCompStompWithHacksLibrary.GlobalState.DEFAULT_VOLUME : this._unmuteVolume;
                        this._unmuteVolume = this._currentVolume;
                    } else {
                        this._unmuteVolume = this._currentVolume;
                        this._currentVolume = 0;
                    }
                }

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && ((this._xPos + 50) | 0) <= mouseX && mouseX <= ((this._xPos + 150) | 0) && ((this._yPos + 10) | 0) <= mouseY && mouseY <= ((this._yPos + 40) | 0)) {
                    this._isDraggingVolumeSlider = true;
                }

                if (this._isDraggingVolumeSlider && mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    var volume = (mouseX - (((this._xPos + 50) | 0))) | 0;
                    if (volume < 0) {
                        volume = 0;
                    }
                    if (volume > 100) {
                        volume = 100;
                    }

                    this._currentVolume = volume;
                    this._unmuteVolume = this._currentVolume;
                }

                if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this._isDraggingVolumeSlider = false;
                }
            },
            /**
             * Returns a number from 0 to 100 (both inclusive)
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.MusicVolumePicker
             * @memberof ChessCompStompWithHacksLibrary.MusicVolumePicker
             * @return  {number}
             */
            GetCurrentMusicVolume: function () {
                return this._currentVolume;
            },
            Render: function (displayOutput) {
                if (this._currentVolume > 0) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImage(ChessCompStompWithHacksLibrary.ChessImage.MusicOn, this._xPos, this._yPos);
                } else {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImage(ChessCompStompWithHacksLibrary.ChessImage.MusicOff, this._xPos, this._yPos);
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this._xPos + 50) | 0), ((this._yPos + 10) | 0), 101, 31, DTLibrary.DTColor.Black(), false);

                if (this._currentVolume > 0) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this._xPos + 50) | 0), ((this._yPos + 10) | 0), this._currentVolume, 31, DTLibrary.DTColor.Black(), true);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.NewGameCreation", {
        statics: {
            methods: {
                CreateNewGame: function (isPlayerWhite, researchedHacks, aiHackLevel) {
                    var $t, $t1, $t2, $t3, $t4, $t5, $t6, $t7, $t8, $t9, $t10, $t11, $t12, $t13, $t14, $t15, $t16, $t17, $t18, $t19, $t20, $t21, $t22, $t23, $t24, $t25, $t26, $t27, $t28, $t29, $t30, $t31, $t32, $t33, $t34, $t35, $t36, $t37, $t38, $t39, $t40, $t41, $t42, $t43, $t44, $t45, $t46, $t47, $t48, $t49, $t50, $t51, $t52, $t53, $t54, $t55, $t56, $t57, $t58, $t59, $t60, $t61, $t62, $t63, $t64, $t65, $t66, $t67, $t68, $t69, $t70, $t71, $t72, $t73, $t74, $t75, $t76, $t77, $t78, $t79, $t80, $t81;
                    var hacks = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Hack)).ctor();
                    for (var i = 0; i < researchedHacks.Count; i = (i + 1) | 0) {
                        hacks.add(researchedHacks.getItem(i));
                    }

                    var board = System.Array.init(8, null, System.Array.type(ChessCompStompWithHacksEngine.ChessSquarePiece));
                    for (var i1 = 0; i1 < 8; i1 = (i1 + 1) | 0) {
                        board[System.Array.index(i1, board)] = System.Array.init(8, 0, ChessCompStompWithHacksEngine.ChessSquarePiece);
                        for (var j = 0; j < 8; j = (j + 1) | 0) {
                            ($t = board[System.Array.index(i1, board)])[System.Array.index(j, $t)] = ChessCompStompWithHacksEngine.ChessSquarePiece.Empty;
                        }
                    }

                    for (var i2 = 0; i2 < 8; i2 = (i2 + 1) | 0) {
                        ($t1 = board[System.Array.index(i2, board)])[System.Array.index(1, $t1)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                        ($t2 = board[System.Array.index(i2, board)])[System.Array.index(6, $t2)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                    }

                    ($t3 = board[System.Array.index(0, board)])[System.Array.index(0, $t3)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                    ($t4 = board[System.Array.index(1, board)])[System.Array.index(0, $t4)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                    ($t5 = board[System.Array.index(2, board)])[System.Array.index(0, $t5)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;
                    ($t6 = board[System.Array.index(3, board)])[System.Array.index(0, $t6)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                    ($t7 = board[System.Array.index(4, board)])[System.Array.index(0, $t7)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing;
                    ($t8 = board[System.Array.index(5, board)])[System.Array.index(0, $t8)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;
                    ($t9 = board[System.Array.index(6, board)])[System.Array.index(0, $t9)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                    ($t10 = board[System.Array.index(7, board)])[System.Array.index(0, $t10)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;

                    ($t11 = board[System.Array.index(0, board)])[System.Array.index(7, $t11)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                    ($t12 = board[System.Array.index(1, board)])[System.Array.index(7, $t12)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                    ($t13 = board[System.Array.index(2, board)])[System.Array.index(7, $t13)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                    ($t14 = board[System.Array.index(3, board)])[System.Array.index(7, $t14)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                    ($t15 = board[System.Array.index(4, board)])[System.Array.index(7, $t15)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing;
                    ($t16 = board[System.Array.index(5, board)])[System.Array.index(7, $t16)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                    ($t17 = board[System.Array.index(6, board)])[System.Array.index(7, $t17)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                    ($t18 = board[System.Array.index(7, board)])[System.Array.index(7, $t18)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;

                    if (hacks.contains(ChessCompStompWithHacksEngine.Hack.ExtraQueen)) {
                        if (isPlayerWhite) {
                            ($t19 = board[System.Array.index(3, board)])[System.Array.index(1, $t19)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                            ($t20 = board[System.Array.index(3, board)])[System.Array.index(2, $t20)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                        } else {
                            ($t21 = board[System.Array.index(3, board)])[System.Array.index(6, $t21)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                            ($t22 = board[System.Array.index(3, board)])[System.Array.index(5, $t22)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                        }
                    }

                    var numberOfExtraPawns = 0;
                    if (hacks.contains(ChessCompStompWithHacksEngine.Hack.ExtraPawnFirst)) {
                        numberOfExtraPawns = (numberOfExtraPawns + 1) | 0;
                    }
                    if (hacks.contains(ChessCompStompWithHacksEngine.Hack.ExtraPawnSecond)) {
                        numberOfExtraPawns = (numberOfExtraPawns + 1) | 0;
                    }

                    while (numberOfExtraPawns > 0) {
                        if (isPlayerWhite) {
                            if (($t23 = board[System.Array.index(3, board)])[System.Array.index(2, $t23)] === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                ($t24 = board[System.Array.index(3, board)])[System.Array.index(2, $t24)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                            } else {
                                if (($t25 = board[System.Array.index(4, board)])[System.Array.index(2, $t25)] === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    ($t26 = board[System.Array.index(4, board)])[System.Array.index(2, $t26)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                                } else {
                                    if (($t27 = board[System.Array.index(2, board)])[System.Array.index(2, $t27)] === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                        ($t28 = board[System.Array.index(2, board)])[System.Array.index(2, $t28)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                                    } else {
                                        throw new System.Exception();
                                    }
                                }
                            }
                        } else {
                            if (($t29 = board[System.Array.index(3, board)])[System.Array.index(5, $t29)] === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                ($t30 = board[System.Array.index(3, board)])[System.Array.index(5, $t30)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                            } else {
                                if (($t31 = board[System.Array.index(4, board)])[System.Array.index(5, $t31)] === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                    ($t32 = board[System.Array.index(4, board)])[System.Array.index(5, $t32)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                                } else {
                                    if (($t33 = board[System.Array.index(2, board)])[System.Array.index(5, $t33)] === ChessCompStompWithHacksEngine.ChessSquarePiece.Empty) {
                                        ($t34 = board[System.Array.index(2, board)])[System.Array.index(5, $t34)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                                    } else {
                                        throw new System.Exception();
                                    }
                                }
                            }
                        }

                        numberOfExtraPawns = (numberOfExtraPawns - 1) | 0;
                    }

                    switch (aiHackLevel) {
                        case ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.Initial: 
                            // make no additional changes to the board
                            break;
                        case ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.UpgradedOnce: 
                            if (isPlayerWhite) {
                                for (var i3 = 0; i3 < 8; i3 = (i3 + 1) | 0) {
                                    ($t35 = board[System.Array.index(i3, board)])[System.Array.index(5, $t35)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                                }
                            } else {
                                for (var i4 = 0; i4 < 8; i4 = (i4 + 1) | 0) {
                                    ($t36 = board[System.Array.index(i4, board)])[System.Array.index(2, $t36)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                                }
                            }
                            break;
                        case ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.UpgradedTwice: 
                            if (isPlayerWhite) {
                                for (var i5 = 0; i5 < 8; i5 = (i5 + 1) | 0) {
                                    ($t37 = board[System.Array.index(i5, board)])[System.Array.index(5, $t37)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                                }

                                ($t38 = board[System.Array.index(0, board)])[System.Array.index(6, $t38)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                                ($t39 = board[System.Array.index(1, board)])[System.Array.index(6, $t39)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                                ($t40 = board[System.Array.index(2, board)])[System.Array.index(6, $t40)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                                ($t41 = board[System.Array.index(3, board)])[System.Array.index(6, $t41)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                                ($t42 = board[System.Array.index(4, board)])[System.Array.index(6, $t42)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                                ($t43 = board[System.Array.index(5, board)])[System.Array.index(6, $t43)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                                ($t44 = board[System.Array.index(6, board)])[System.Array.index(6, $t44)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                                ($t45 = board[System.Array.index(7, board)])[System.Array.index(6, $t45)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                            } else {
                                for (var i6 = 0; i6 < 8; i6 = (i6 + 1) | 0) {
                                    ($t46 = board[System.Array.index(i6, board)])[System.Array.index(2, $t46)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                                }

                                ($t47 = board[System.Array.index(0, board)])[System.Array.index(1, $t47)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                                ($t48 = board[System.Array.index(1, board)])[System.Array.index(1, $t48)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                                ($t49 = board[System.Array.index(2, board)])[System.Array.index(1, $t49)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;
                                ($t50 = board[System.Array.index(3, board)])[System.Array.index(1, $t50)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                                ($t51 = board[System.Array.index(4, board)])[System.Array.index(1, $t51)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                                ($t52 = board[System.Array.index(5, board)])[System.Array.index(1, $t52)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;
                                ($t53 = board[System.Array.index(6, board)])[System.Array.index(1, $t53)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                                ($t54 = board[System.Array.index(7, board)])[System.Array.index(1, $t54)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                            }
                            break;
                        case ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.UpgradedThrice: 
                            if (isPlayerWhite) {
                                for (var i7 = 0; i7 < 8; i7 = (i7 + 1) | 0) {
                                    ($t55 = board[System.Array.index(i7, board)])[System.Array.index(4, $t55)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                                }

                                for (var j1 = 5; j1 <= 6; j1 = (j1 + 1) | 0) {
                                    ($t56 = board[System.Array.index(0, board)])[System.Array.index(j1, $t56)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                                    ($t57 = board[System.Array.index(1, board)])[System.Array.index(j1, $t57)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                                    ($t58 = board[System.Array.index(2, board)])[System.Array.index(j1, $t58)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                                    ($t59 = board[System.Array.index(3, board)])[System.Array.index(j1, $t59)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                                    ($t60 = board[System.Array.index(4, board)])[System.Array.index(j1, $t60)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                                    ($t61 = board[System.Array.index(5, board)])[System.Array.index(j1, $t61)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop;
                                    ($t62 = board[System.Array.index(6, board)])[System.Array.index(j1, $t62)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight;
                                    ($t63 = board[System.Array.index(7, board)])[System.Array.index(j1, $t63)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook;
                                }
                            } else {
                                for (var i8 = 0; i8 < 8; i8 = (i8 + 1) | 0) {
                                    ($t64 = board[System.Array.index(i8, board)])[System.Array.index(3, $t64)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                                }

                                for (var j2 = 1; j2 <= 2; j2 = (j2 + 1) | 0) {
                                    ($t65 = board[System.Array.index(0, board)])[System.Array.index(j2, $t65)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                                    ($t66 = board[System.Array.index(1, board)])[System.Array.index(j2, $t66)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                                    ($t67 = board[System.Array.index(2, board)])[System.Array.index(j2, $t67)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;
                                    ($t68 = board[System.Array.index(3, board)])[System.Array.index(j2, $t68)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                                    ($t69 = board[System.Array.index(4, board)])[System.Array.index(j2, $t69)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                                    ($t70 = board[System.Array.index(5, board)])[System.Array.index(j2, $t70)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop;
                                    ($t71 = board[System.Array.index(6, board)])[System.Array.index(j2, $t71)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight;
                                    ($t72 = board[System.Array.index(7, board)])[System.Array.index(j2, $t72)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook;
                                }
                            }
                            break;
                        case ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.FinalBattle: 
                            if (isPlayerWhite) {
                                for (var i9 = 0; i9 < 8; i9 = (i9 + 1) | 0) {
                                    ($t73 = board[System.Array.index(i9, board)])[System.Array.index(4, $t73)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                                }

                                for (var i10 = 0; i10 < 8; i10 = (i10 + 1) | 0) {
                                    for (var j3 = 5; j3 <= 7; j3 = (j3 + 1) | 0) {
                                        if (($t74 = board[System.Array.index(i10, board)])[System.Array.index(j3, $t74)] !== ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing) {
                                            ($t75 = board[System.Array.index(i10, board)])[System.Array.index(j3, $t75)] = ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen;
                                        }
                                    }
                                }
                            } else {
                                for (var i11 = 0; i11 < 8; i11 = (i11 + 1) | 0) {
                                    ($t76 = board[System.Array.index(i11, board)])[System.Array.index(3, $t76)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn;
                                }

                                for (var i12 = 0; i12 < 8; i12 = (i12 + 1) | 0) {
                                    for (var j4 = 0; j4 <= 2; j4 = (j4 + 1) | 0) {
                                        if (($t77 = board[System.Array.index(i12, board)])[System.Array.index(j4, $t77)] !== ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing) {
                                            ($t78 = board[System.Array.index(i12, board)])[System.Array.index(j4, $t78)] = ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen;
                                        }
                                    }
                                }
                            }
                            break;
                        default: 
                            throw new System.Exception();
                    }

                    var unmovedPawns = System.Array.init(8, null, System.Array.type(System.Boolean));
                    for (var i13 = 0; i13 < 8; i13 = (i13 + 1) | 0) {
                        unmovedPawns[System.Array.index(i13, unmovedPawns)] = System.Array.init(8, false, System.Boolean);
                        for (var j5 = 0; j5 < 8; j5 = (j5 + 1) | 0) {
                            ($t79 = unmovedPawns[System.Array.index(i13, unmovedPawns)])[System.Array.index(j5, $t79)] = ($t80 = board[System.Array.index(i13, board)])[System.Array.index(j5, $t80)] === ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn || ($t81 = board[System.Array.index(i13, board)])[System.Array.index(j5, $t81)] === ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn;
                        }
                    }

                    return new ChessCompStompWithHacksEngine.GameState(new ChessCompStompWithHacksEngine.ChessSquarePieceArray.$ctor1(board), new ChessCompStompWithHacksEngine.UnmovedPawnsArray.$ctor1(unmovedPawns), 1, false, isPlayerWhite, true, null, new ChessCompStompWithHacksEngine.GameState.CastlingRights(true, true, true, true), new ChessCompStompWithHacksEngine.GameState.PlayerAbilities(hacks.contains(ChessCompStompWithHacksEngine.Hack.PawnsCanMoveThreeSpacesInitially), hacks.contains(ChessCompStompWithHacksEngine.Hack.SuperEnPassant), hacks.contains(ChessCompStompWithHacksEngine.Hack.RooksCanMoveLikeBishops), hacks.contains(ChessCompStompWithHacksEngine.Hack.SuperCastling), hacks.contains(ChessCompStompWithHacksEngine.Hack.RooksCanCaptureLikeCannons), hacks.contains(ChessCompStompWithHacksEngine.Hack.KnightsCanMakeLargeKnightsMove), hacks.contains(ChessCompStompWithHacksEngine.Hack.QueensCanMoveLikeKnights), hacks.contains(ChessCompStompWithHacksEngine.Hack.TacticalNuke), hacks.contains(ChessCompStompWithHacksEngine.Hack.AnyPieceCanPromote), hacks.contains(ChessCompStompWithHacksEngine.Hack.StalemateIsVictory), hacks.contains(ChessCompStompWithHacksEngine.Hack.OpponentMustCaptureWhenPossible), hacks.contains(ChessCompStompWithHacksEngine.Hack.PawnsDestroyCapturingPiece)));
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.NukeRenderer", {
        statics: {
            fields: {
                ELAPSED_MICROS_TO_FLY_OFF_SCREEN: 0
            },
            ctors: {
                init: function () {
                    this.ELAPSED_MICROS_TO_FLY_OFF_SCREEN = 300000;
                }
            },
            methods: {
                GetNukeRenderer: function (hasNukeAbility, hasUsedNuke, isNukeSelected, isHoverOverNuke, turnCount, timer) {
                    return new ChessCompStompWithHacksLibrary.NukeRenderer(hasNukeAbility, hasUsedNuke, isNukeSelected, isHoverOverNuke, turnCount, timer, null);
                },
                IsHoverOverNuke: function (mouse) {
                    var $t;
                    var mouseX = mouse.DTLibrary$IMouse$GetX();
                    var mouseY = mouse.DTLibrary$IMouse$GetY();

                    var hitboxes = new (System.Collections.Generic.List$1(System.Tuple$4(System.Int32,System.Int32,System.Int32,System.Int32))).ctor();

                    hitboxes.add({ Item1: 38, Item2: 0, Item3: 98, Item4: 18 });
                    hitboxes.add({ Item1: 0, Item2: 18, Item3: 136, Item4: 54 });
                    hitboxes.add({ Item1: 0, Item2: 54, Item3: 136, Item4: 67 });
                    hitboxes.add({ Item1: 9, Item2: 67, Item3: 127, Item4: 77 });
                    hitboxes.add({ Item1: 16, Item2: 77, Item3: 120, Item4: 87 });
                    hitboxes.add({ Item1: 23, Item2: 87, Item3: 113, Item4: 97 });
                    hitboxes.add({ Item1: 29, Item2: 97, Item3: 107, Item4: 104 });
                    hitboxes.add({ Item1: 33, Item2: 104, Item3: 103, Item4: 318 });
                    hitboxes.add({ Item1: 40, Item2: 318, Item3: 96, Item4: 324 });
                    hitboxes.add({ Item1: 46, Item2: 324, Item3: 90, Item4: 355 });
                    hitboxes.add({ Item1: 50, Item2: 355, Item3: 86, Item4: 363 });
                    hitboxes.add({ Item1: 57, Item2: 363, Item3: 79, Item4: 366 });
                    hitboxes.add({ Item1: 61, Item2: 366, Item3: 75, Item4: 369 });

                    $t = Bridge.getEnumerator(hitboxes);
                    try {
                        while ($t.moveNext()) {
                            var hitbox = $t.Current;
                            if (mouseX >= hitbox.Item1 && mouseX <= hitbox.Item3 && mouseY >= hitbox.Item2 && mouseY <= hitbox.Item4) {
                                return true;
                            }
                        }
                    } finally {
                        if (Bridge.is($t, System.IDisposable)) {
                            $t.System$IDisposable$Dispose();
                        }
                    }

                    return false;
                }
            }
        },
        fields: {
            hasNukeAbility: false,
            hasUsedNuke: false,
            isNukeSelected: false,
            isHoverOverNuke: null,
            turnCount: 0,
            timer: null,
            nukeAnimationElapsedMicros: null
        },
        ctors: {
            ctor: function (hasNukeAbility, hasUsedNuke, isNukeSelected, isHoverOverNuke, turnCount, timer, nukeAnimationElapsedMicros) {
                this.$initialize();
                this.hasNukeAbility = hasNukeAbility;
                this.hasUsedNuke = hasUsedNuke;
                this.isNukeSelected = isNukeSelected;
                this.isHoverOverNuke = isHoverOverNuke;
                this.turnCount = turnCount;
                this.timer = timer;
                this.nukeAnimationElapsedMicros = nukeAnimationElapsedMicros;
            }
        },
        methods: {
            LaunchNuke: function () {
                return new ChessCompStompWithHacksLibrary.NukeRenderer(this.hasNukeAbility, this.hasUsedNuke, this.isNukeSelected, this.isHoverOverNuke, this.turnCount, this.timer, 0);
            },
            ProcessFrame: function (hasUsedNuke, isNukeSelected, isHoverOverNuke, turnCount, elapsedMicrosPerFrame) {
                var newNukeAnimationElapsedMicros;
                if (this.nukeAnimationElapsedMicros == null) {
                    newNukeAnimationElapsedMicros = null;
                } else {
                    newNukeAnimationElapsedMicros = Bridge.Int.clip32(System.Nullable.getValue(this.nukeAnimationElapsedMicros) + elapsedMicrosPerFrame);
                }

                if (System.Nullable.hasValue(newNukeAnimationElapsedMicros) && System.Nullable.getValue(newNukeAnimationElapsedMicros) > ChessCompStompWithHacksLibrary.NukeRenderer.ELAPSED_MICROS_TO_FLY_OFF_SCREEN) {
                    newNukeAnimationElapsedMicros = 300001;
                }

                return new ChessCompStompWithHacksLibrary.NukeRenderer(this.hasNukeAbility, hasUsedNuke, isNukeSelected, isHoverOverNuke, turnCount, this.timer, newNukeAnimationElapsedMicros);
            },
            HasNukeFlownOffScreen: function () {
                if (this.nukeAnimationElapsedMicros == null) {
                    return false;
                }

                return System.Nullable.getValue(this.nukeAnimationElapsedMicros) >= ChessCompStompWithHacksLibrary.NukeRenderer.ELAPSED_MICROS_TO_FLY_OFF_SCREEN;
            },
            Render: function (displayOutput) {
                if (!this.hasNukeAbility) {
                    return;
                }

                if (this.nukeAnimationElapsedMicros == null) {
                    if (this.hasUsedNuke) {
                        return;
                    }

                    var isNukeAvailable = this.turnCount > ChessCompStompWithHacksEngine.TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable;

                    var nukeImage = new ChessCompStompWithHacksLibrary.ChessImage();

                    if (isNukeAvailable) {
                        if (this.isNukeSelected) {
                            nukeImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Selected;
                        } else {
                            if (this.isHoverOverNuke != null) {
                                nukeImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Hover;
                            } else {
                                nukeImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready;
                            }
                        }
                    } else {
                        nukeImage = ChessCompStompWithHacksLibrary.ChessImage.Nuke_NotReady;
                    }

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(nukeImage, 0, 0, 0, 128);

                    if (this.isHoverOverNuke != null && !isNukeAvailable) {
                        var numTurnsUntilNukeAvailable = (11 - this.turnCount) | 0;
                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.isHoverOverNuke.Item1, this.isHoverOverNuke.Item2, ((335 + (numTurnsUntilNukeAvailable >= 10 ? 8 : 0)) | 0), 21, new DTLibrary.DTColor.ctor(255, 245, 171), true);

                        var text = numTurnsUntilNukeAvailable > 1 ? "Tactical nuke available in " + (DTLibrary.StringUtil.ToStringCultureInvariant(numTurnsUntilNukeAvailable) || "") + " turns" : "Tactical nuke available in 1 turn";

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.isHoverOverNuke.Item1 + 5) | 0), ((this.isHoverOverNuke.Item2 + 19) | 0), text, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, DTLibrary.DTColor.Black());
                    }
                } else {
                    if (System.Nullable.getValue(this.nukeAnimationElapsedMicros) >= ChessCompStompWithHacksLibrary.NukeRenderer.ELAPSED_MICROS_TO_FLY_OFF_SCREEN) {
                        return;
                    }

                    var rocketWidth = displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready);

                    var rocketFireScalingFactor = 256;
                    var rocketFireWidthOriginal = displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire);
                    var rocketFireHeightOriginal = displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire);
                    var rocketFireWidthScaled = (Bridge.Int.div(Bridge.Int.mul(rocketFireWidthOriginal, rocketFireScalingFactor), 128)) | 0;
                    var rocketFireHeightScaled = (Bridge.Int.div(Bridge.Int.mul(rocketFireHeightOriginal, rocketFireScalingFactor), 128)) | 0;
                    var endingY = (ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT + rocketFireHeightScaled) | 0;
                    var y = System.Int64.clip32(System.Int64(System.Nullable.getValue(this.nukeAnimationElapsedMicros)).mul(System.Int64(endingY)).div((System.Int64(300000))));

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(ChessCompStompWithHacksLibrary.ChessImage.Nuke_Ready, 0, y, 0, 128);

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(ChessCompStompWithHacksLibrary.ChessImage.Nuke_RocketFire, ((Bridge.Int.div((((rocketWidth - rocketFireWidthScaled) | 0)), 2)) | 0), ((y - rocketFireHeightScaled) | 0), 0, rocketFireScalingFactor);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ObjectiveDisplay", {
        statics: {
            fields: {
                NON_FINAL_OBJECTIVE_WIDTH: 0,
                NON_FINAL_OBJECTIVE_HEIGHT: 0,
                FINAL_OBJECTIVE_WIDTH: 0,
                FINAL_OBJECTIVE_HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.NON_FINAL_OBJECTIVE_WIDTH = 250;
                    this.NON_FINAL_OBJECTIVE_HEIGHT = 100;
                    this.FINAL_OBJECTIVE_WIDTH = 500;
                    this.FINAL_OBJECTIVE_HEIGHT = 100;
                }
            },
            methods: {
                RenderNonFinalObjective: function (x, y, objective, completedObjectives, displayOutput) {
                    var hasCompletedObjective = completedObjectives.contains(objective);

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(x, y, 249, 99, hasCompletedObjective ? new DTLibrary.DTColor.ctor(201, 255, 196) : new DTLibrary.DTColor.ctor(255, 211, 161), true);

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(x, y, ChessCompStompWithHacksLibrary.ObjectiveDisplay.NON_FINAL_OBJECTIVE_WIDTH, ChessCompStompWithHacksLibrary.ObjectiveDisplay.NON_FINAL_OBJECTIVE_HEIGHT, new DTLibrary.DTColor.ctor(110, 110, 110), false);

                    var objectiveDescription;
                    switch (objective) {
                        case ChessCompStompWithHacksEngine.Objective.DefeatComputer: 
                            objectiveDescription = "Win a game against\nthe AI.";
                            break;
                        case ChessCompStompWithHacksEngine.Objective.DefeatComputerByPlayingAtMost25Moves: 
                            objectiveDescription = "Win by playing at\nmost 25 moves.";
                            break;
                        case ChessCompStompWithHacksEngine.Objective.DefeatComputerWith5QueensOnTheBoard: 
                            objectiveDescription = "Win with 5 queens on\nthe board.";
                            break;
                        case ChessCompStompWithHacksEngine.Objective.CheckmateUsingAKnight: 
                            objectiveDescription = "Deliver checkmate\nusing a knight.";
                            break;
                        case ChessCompStompWithHacksEngine.Objective.PromoteAPieceToABishop: 
                            objectiveDescription = "Promote a piece to\na bishop.";
                            break;
                        case ChessCompStompWithHacksEngine.Objective.LaunchANuke: 
                            objectiveDescription = "Launch a nuke.";
                            break;
                        case ChessCompStompWithHacksEngine.Objective.WinFinalBattle: 
                            throw new System.Exception();
                        default: 
                            throw new System.Exception();
                    }

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 10) | 0), ((y + 90) | 0), objectiveDescription, ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt, DTLibrary.DTColor.Black());

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 10) | 0), ((y + 39) | 0), (DTLibrary.StringUtil.ToStringCultureInvariant(ChessCompStompWithHacksLibrary.SessionState.NUMBER_OF_HACK_POINTS_PER_OBJECTIVE) || "") + " hack points", ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, new DTLibrary.DTColor.ctor(128, 128, 128));

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 10) | 0), ((y + 20) | 0), hasCompletedObjective ? "(completed)" : "(incomplete)", ChessCompStompWithHacksLibrary.ChessFont.Fetamont12Pt, new DTLibrary.DTColor.ctor(128, 128, 128));
                },
                HasUnlockedFinalObjective: function (completedObjectives) {
                    return completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.DefeatComputer) && completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.DefeatComputerByPlayingAtMost25Moves) && completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.DefeatComputerWith5QueensOnTheBoard) && completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.CheckmateUsingAKnight) && completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.PromoteAPieceToABishop) && completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.LaunchANuke);
                },
                RenderFinalObjective: function (x, y, completedObjectives, displayOutput) {
                    var hasCompletedObjective = completedObjectives.contains(ChessCompStompWithHacksEngine.Objective.WinFinalBattle);

                    var hasUnlockedFinalObjective = ChessCompStompWithHacksLibrary.ObjectiveDisplay.HasUnlockedFinalObjective(completedObjectives);

                    var fillColor;

                    if (hasCompletedObjective) {
                        fillColor = new DTLibrary.DTColor.ctor(201, 255, 196);
                    } else {
                        if (!hasUnlockedFinalObjective) {
                            fillColor = new DTLibrary.DTColor.ctor(179, 179, 179);
                        } else {
                            fillColor = new DTLibrary.DTColor.ctor(252, 185, 149);
                        }
                    }

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(x, y, 499, 99, fillColor, true);

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(x, y, ChessCompStompWithHacksLibrary.ObjectiveDisplay.FINAL_OBJECTIVE_WIDTH, ChessCompStompWithHacksLibrary.ObjectiveDisplay.FINAL_OBJECTIVE_HEIGHT, new DTLibrary.DTColor.ctor(110, 110, 110), false);

                    if (hasUnlockedFinalObjective) {
                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 10) | 0), ((y + 90) | 0), "Win the Final Battle against the AI.", ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt, DTLibrary.DTColor.Black());

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 10) | 0), ((y + 30) | 0), hasCompletedObjective ? "(completed)" : "(incomplete)", ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, new DTLibrary.DTColor.ctor(128, 128, 128));
                    } else {
                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((x + 237) | 0), ((y + 76) | 0), "?", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());
                    }
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.PromotionPanel", {
        statics: {
            fields: {
                PROMOTION_PANEL_WIDTH: 0,
                PROMOTION_PANEL_HEIGHT: 0,
                QUEEN_OFFSET_X: 0,
                ROOK_OFFSET_X: 0,
                KNIGHT_OFFSET_X: 0,
                BISHOP_OFFSET_X: 0,
                PIECE_OFFSET_Y: 0
            },
            ctors: {
                init: function () {
                    this.PROMOTION_PANEL_WIDTH = 293;
                    this.PROMOTION_PANEL_HEIGHT = 100;
                    this.QUEEN_OFFSET_X = 10;
                    this.ROOK_OFFSET_X = 80;
                    this.KNIGHT_OFFSET_X = 150;
                    this.BISHOP_OFFSET_X = 220;
                    this.PIECE_OFFSET_Y = 8;
                }
            },
            methods: {
                GetPromotionPanel: function (isWhite) {
                    return new ChessCompStompWithHacksLibrary.PromotionPanel(isWhite, false, 0, 0, null, null);
                },
                /**
                 * Returns the piece that the mouse is hovering over, if any
                 *
                 * @static
                 * @public
                 * @this ChessCompStompWithHacksLibrary.PromotionPanel
                 * @memberof ChessCompStompWithHacksLibrary.PromotionPanel
                 * @param   {number}                            promotionPanelX      
                 * @param   {number}                            promotionPanelY      
                 * @param   {DTLibrary.IMouse}                  mouse                
                 * @param   {DTLibrary.IDisplayProcessing$1}    displayProcessing
                 * @return  {?number}
                 */
                IsHoverOverSquare: function (promotionPanelX, promotionPanelY, mouse, displayProcessing) {
                    var imageWidth = (Bridge.Int.div(Bridge.Int.mul(displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;
                    var imageHeight = (Bridge.Int.div(Bridge.Int.mul(displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;

                    var mouseX = mouse.DTLibrary$IMouse$GetX();
                    var mouseY = mouse.DTLibrary$IMouse$GetY();

                    if (mouseY < ((((promotionPanelY - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0)) {
                        return null;
                    }
                    if (mouseY > ((((((promotionPanelY - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0) + imageHeight) | 0)) {
                        return null;
                    }

                    var mouseXRelativeToPanel = (mouseX - promotionPanelX) | 0;

                    if (ChessCompStompWithHacksLibrary.PromotionPanel.QUEEN_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ((ChessCompStompWithHacksLibrary.PromotionPanel.QUEEN_OFFSET_X + imageWidth) | 0)) {
                        return ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen;
                    }
                    if (ChessCompStompWithHacksLibrary.PromotionPanel.ROOK_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ((ChessCompStompWithHacksLibrary.PromotionPanel.ROOK_OFFSET_X + imageWidth) | 0)) {
                        return ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToRook;
                    }
                    if (ChessCompStompWithHacksLibrary.PromotionPanel.KNIGHT_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ((ChessCompStompWithHacksLibrary.PromotionPanel.KNIGHT_OFFSET_X + imageWidth) | 0)) {
                        return ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToKnight;
                    }
                    if (ChessCompStompWithHacksLibrary.PromotionPanel.BISHOP_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ((ChessCompStompWithHacksLibrary.PromotionPanel.BISHOP_OFFSET_X + imageWidth) | 0)) {
                        return ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToBishop;
                    }
                    return null;
                },
                IsHoverOverPanel: function (promotionPanelX, promotionPanelY, mouse) {
                    var mouseX = mouse.DTLibrary$IMouse$GetX();
                    var mouseY = mouse.DTLibrary$IMouse$GetY();

                    if (mouseX < promotionPanelX) {
                        return false;
                    }
                    if (mouseX > ((promotionPanelX + ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_WIDTH) | 0)) {
                        return false;
                    }
                    if (mouseY < ((promotionPanelY - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0)) {
                        return false;
                    }
                    if (mouseY > promotionPanelY) {
                        return false;
                    }
                    return true;
                },
                GetXOffset: function (promotionType) {
                    switch (promotionType) {
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToQueen: 
                            return ChessCompStompWithHacksLibrary.PromotionPanel.QUEEN_OFFSET_X;
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToRook: 
                            return ChessCompStompWithHacksLibrary.PromotionPanel.ROOK_OFFSET_X;
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToKnight: 
                            return ChessCompStompWithHacksLibrary.PromotionPanel.KNIGHT_OFFSET_X;
                        case ChessCompStompWithHacksEngine.Move.PromotionType.PromoteToBishop: 
                            return ChessCompStompWithHacksLibrary.PromotionPanel.BISHOP_OFFSET_X;
                        default: 
                            throw new System.Exception();
                    }
                }
            }
        },
        fields: {
            isWhite: false,
            isOpen: false,
            x: 0,
            y: 0,
            hoverSquare: null,
            selectedSquare: null
        },
        ctors: {
            ctor: function (isWhite, isOpen, x, y, hoverSquare, selectedSquare) {
                this.$initialize();
                this.isWhite = isWhite;
                this.isOpen = isOpen;
                this.x = x;
                this.y = y;
                this.hoverSquare = hoverSquare;
                this.selectedSquare = selectedSquare;
            }
        },
        methods: {
            ProcessFrame: function (isOpen, x, y, hoverSquare, selectedSquare) {
                return new ChessCompStompWithHacksLibrary.PromotionPanel(this.isWhite, isOpen, x, y, hoverSquare, selectedSquare);
            },
            Render: function (displayOutput) {
                if (!this.isOpen) {
                    return;
                }

                var imageWidth = (Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;
                var imageHeight = (Bridge.Int.div(Bridge.Int.mul(displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.WhitePawn), ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor), 128)) | 0;

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, ((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0), ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_WIDTH, ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT, new DTLibrary.DTColor.ctor(255, 245, 171), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + 90) | 0), ((this.y - 10) | 0), "Promote to:", ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(this.isWhite ? ChessCompStompWithHacksLibrary.ChessImage.WhiteQueen : ChessCompStompWithHacksLibrary.ChessImage.BlackQueen, ((this.x + ChessCompStompWithHacksLibrary.PromotionPanel.QUEEN_OFFSET_X) | 0), ((((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0), 0, ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor);
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(this.isWhite ? ChessCompStompWithHacksLibrary.ChessImage.WhiteRook : ChessCompStompWithHacksLibrary.ChessImage.BlackRook, ((this.x + ChessCompStompWithHacksLibrary.PromotionPanel.ROOK_OFFSET_X) | 0), ((((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0), 0, ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor);
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(this.isWhite ? ChessCompStompWithHacksLibrary.ChessImage.WhiteKnight : ChessCompStompWithHacksLibrary.ChessImage.BlackKnight, ((this.x + ChessCompStompWithHacksLibrary.PromotionPanel.KNIGHT_OFFSET_X) | 0), ((((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0), 0, ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor);
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1(this.isWhite ? ChessCompStompWithHacksLibrary.ChessImage.WhiteBishop : ChessCompStompWithHacksLibrary.ChessImage.BlackBishop, ((this.x + ChessCompStompWithHacksLibrary.PromotionPanel.BISHOP_OFFSET_X) | 0), ((((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0), 0, ChessCompStompWithHacksLibrary.ChessImageUtil.ChessPieceScalingFactor);

                if (this.hoverSquare != null && (this.selectedSquare == null || System.Nullable.getValue(this.selectedSquare) !== System.Nullable.getValue(this.hoverSquare))) {
                    var hoverXOffset = ChessCompStompWithHacksLibrary.PromotionPanel.GetXOffset(System.Nullable.getValue(this.hoverSquare));

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this.x + hoverXOffset) | 0), ((((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0), imageWidth, imageHeight, new DTLibrary.DTColor.$ctor1(0, 0, 128, 50), true);
                }

                if (this.selectedSquare != null) {
                    var selectedXOffset = ChessCompStompWithHacksLibrary.PromotionPanel.GetXOffset(System.Nullable.getValue(this.selectedSquare));

                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this.x + selectedXOffset) | 0), ((((this.y - ChessCompStompWithHacksLibrary.PromotionPanel.PROMOTION_PANEL_HEIGHT) | 0) + ChessCompStompWithHacksLibrary.PromotionPanel.PIECE_OFFSET_Y) | 0), imageWidth, imageHeight, new DTLibrary.DTColor.$ctor1(0, 0, 170, 150), true);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SessionState", {
        statics: {
            fields: {
                NUMBER_OF_HACK_POINTS_PER_WIN: 0,
                NUMBER_OF_HACK_POINTS_PER_OBJECTIVE: 0
            },
            ctors: {
                init: function () {
                    this.NUMBER_OF_HACK_POINTS_PER_WIN = 5;
                    this.NUMBER_OF_HACK_POINTS_PER_OBJECTIVE = 10;
                }
            }
        },
        fields: {
            timer: null,
            data: null
        },
        props: {
            HasStarted: {
                get: function () {
                    return System.Nullable.liftne("ne", this.data.StartTime, System.Int64.lift(null));
                }
            }
        },
        ctors: {
            ctor: function (timer) {
                this.$initialize();
                this.timer = timer;
                this.data = new ChessCompStompWithHacksLibrary.SessionState.Data();
            }
        },
        methods: {
            ClearData: function () {
                this.data = new ChessCompStompWithHacksLibrary.SessionState.Data();
            },
            Debug_AddWin: function () {
                this.data.NumberOfWins = (this.data.NumberOfWins + 1) | 0;
            },
            GetGameLogic: function () {
                return this.data.GameLogic;
            },
            GetMostRecentGameLogic: function () {
                return this.data.MostRecentGameLogic;
            },
            AddCompletedObjectives: function (completedObjectives) {
                var $t;
                $t = Bridge.getEnumerator(completedObjectives);
                try {
                    while ($t.moveNext()) {
                        var completedObjective = $t.Current;
                        this.data.CompletedObjectives.add(completedObjective);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }
            },
            GetCompletedObjectives: function () {
                return new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Objective)).$ctor1(this.data.CompletedObjectives);
            },
            StartNewGame: function () {
                this.data.StartTime = this.timer.DTLibrary$ITimer$GetNumberOfMicroSeconds();
            },
            GetResearchedHacks: function () {
                return new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Hack)).$ctor1(this.data.ResearchedHacks);
            },
            AddResearchedHack: function (hack) {
                this.data.ResearchedHacks.add(hack);
            },
            /**
             * Includes both used and unused hack points
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.SessionState
             * @memberof ChessCompStompWithHacksLibrary.SessionState
             * @return  {number}
             */
            GetTotalNumberOfHackPoints: function () {
                var initialNumberOfHackPoints = 10;

                var numberOfPointsFromWins = Bridge.Int.mul(this.data.NumberOfWins, ChessCompStompWithHacksLibrary.SessionState.NUMBER_OF_HACK_POINTS_PER_WIN);

                var numberOfPointsFromObjectives;

                if (this.data.CompletedObjectives.contains(ChessCompStompWithHacksEngine.Objective.WinFinalBattle)) {
                    numberOfPointsFromObjectives = Bridge.Int.mul((((this.data.CompletedObjectives.Count - 1) | 0)), ChessCompStompWithHacksLibrary.SessionState.NUMBER_OF_HACK_POINTS_PER_OBJECTIVE);
                } else {
                    numberOfPointsFromObjectives = Bridge.Int.mul(this.data.CompletedObjectives.Count, ChessCompStompWithHacksLibrary.SessionState.NUMBER_OF_HACK_POINTS_PER_OBJECTIVE);
                }

                return ((((initialNumberOfHackPoints + numberOfPointsFromWins) | 0) + numberOfPointsFromObjectives) | 0);
            },
            GetUnusedHackPoints: function () {
                var $t;
                var points = this.GetTotalNumberOfHackPoints();
                $t = Bridge.getEnumerator(this.data.ResearchedHacks);
                try {
                    while ($t.moveNext()) {
                        var hack = $t.Current;
                        points = (points - ChessCompStompWithHacksLibrary.HackUtil.GetHackCost(hack)) | 0;
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                return points;
            },
            ResetResearchedHacks: function () {
                this.data.ResearchedHacks = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Hack)).ctor();
            },
            HasShownFinalBattleVictoryPanel: function () {
                return this.data.HasShownFinalBattleVictoryPanel;
            },
            SetShownFinalBattleVictoryPanel: function () {
                this.data.HasShownFinalBattleVictoryPanel = true;
            },
            CompleteGame: function (didPlayerWin) {
                if (didPlayerWin) {
                    this.data.NumberOfWins = (this.data.NumberOfWins + 1) | 0;
                }

                this.data.MostRecentGameLogic = this.data.GameLogic;
                this.data.GameLogic = null;
            },
            StartGame: function (isFinalBattle, globalState) {
                var isPlayerWhite;

                if (this.data.WasPlayerWhiteInPreviousGame == null) {
                    isPlayerWhite = true;
                } else {
                    isPlayerWhite = !System.Nullable.getValue(this.data.WasPlayerWhiteInPreviousGame);
                }

                if (isFinalBattle) {
                    this.data.GameLogic = new ChessCompStompWithHacksLibrary.GameLogic(globalState, isPlayerWhite, new (DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.Hack)).$ctor1(this.data.ResearchedHacks), ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.FinalBattle);
                    this.data.MostRecentGameLogic = this.data.GameLogic;

                    this.data.WasPlayerWhiteInPreviousGame = isPlayerWhite;
                    if (this.data.HasShownFinalBattleMessage) {
                        return new ChessCompStompWithHacksLibrary.ChessFrame(globalState, this);
                    }

                    this.data.HasShownFinalBattleMessage = true;
                    return ChessCompStompWithHacksLibrary.AIMessageFrame.GetFinalBattleMessageFrame(globalState, this);
                }

                var aiHackLevel = new ChessCompStompWithHacksLibrary.SessionState.AIHackLevel();

                if (!this.data.HasShownAIHackMessage && this.data.ResearchedHacks.Count === 0 || this.data.NumberOfWins <= 1) {
                    aiHackLevel = ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.Initial;
                } else {
                    if (this.data.NumberOfWins <= 3) {
                        aiHackLevel = ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.UpgradedOnce;
                    } else {
                        if (this.data.NumberOfWins <= 5) {
                            aiHackLevel = ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.UpgradedTwice;
                        } else {
                            aiHackLevel = ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.UpgradedThrice;
                        }
                    }
                }

                this.data.GameLogic = new ChessCompStompWithHacksLibrary.GameLogic(globalState, isPlayerWhite, new (DTLibrary.DTImmutableList$1(ChessCompStompWithHacksEngine.Hack)).$ctor1(this.data.ResearchedHacks), aiHackLevel);
                this.data.MostRecentGameLogic = this.data.GameLogic;

                this.data.WasPlayerWhiteInPreviousGame = isPlayerWhite;

                if (aiHackLevel !== ChessCompStompWithHacksLibrary.SessionState.AIHackLevel.Initial && !this.data.HasShownAIHackMessage) {
                    this.data.HasShownAIHackMessage = true;
                    return ChessCompStompWithHacksLibrary.AIMessageFrame.GetAIHackMessageFrame(globalState, this);
                }

                return new ChessCompStompWithHacksLibrary.ChessFrame(globalState, this);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SessionState.AIHackLevel", {
        $kind: "nested enum",
        statics: {
            fields: {
                Initial: 0,
                UpgradedOnce: 1,
                UpgradedTwice: 2,
                UpgradedThrice: 3,
                FinalBattle: 4
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SessionState.Data", {
        $kind: "nested class",
        fields: {
            /**
             * Null if player hasn't started playing yet
             or if player chose to "clear saved data".
             *
             * @instance
             * @public
             * @memberof ChessCompStompWithHacksLibrary.SessionState.Data
             * @type ?System.Int64
             */
            StartTime: null,
            NumberOfWins: 0,
            WasPlayerWhiteInPreviousGame: null,
            HasShownAIHackMessage: false,
            HasShownFinalBattleMessage: false,
            HasShownFinalBattleVictoryPanel: false,
            ResearchedHacks: null,
            CompletedObjectives: null,
            GameLogic: null,
            MostRecentGameLogic: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                this.ResearchedHacks = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Hack)).ctor();
                this.CompletedObjectives = new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Objective)).ctor();
                this.WasPlayerWhiteInPreviousGame = null;
                this.HasShownAIHackMessage = false;
                this.HasShownFinalBattleMessage = false;
                this.HasShownFinalBattleVictoryPanel = false;
                this.NumberOfWins = 0;
                this.StartTime = System.Int64.lift(null);

                this.GameLogic = null;
                this.MostRecentGameLogic = null;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SettingsIcon", {
        fields: {
            isHover: false,
            isClicked: false
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                this.isHover = false;
                this.isClicked = false;
            }
        },
        methods: {
            /**
             * Returns whether or not the user has clicked the settings icon
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.SettingsIcon
             * @memberof ChessCompStompWithHacksLibrary.SettingsIcon
             * @param   {DTLibrary.IMouse}                  mouseInput            
             * @param   {DTLibrary.IMouse}                  previousMouseInput    
             * @param   {boolean}                           ignoreMouse           
             * @param   {DTLibrary.IDisplayProcessing$1}    displayProcessing
             * @return  {boolean}
             */
            ProcessFrame: function (mouseInput, previousMouseInput, ignoreMouse, displayProcessing) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                var settingsIconWidth = displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Gear);
                var settingsIconHeight = displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.Gear);

                var isHover = ((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH - settingsIconWidth) | 0) <= mouseX && mouseX <= ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH && ((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT - settingsIconHeight) | 0) <= mouseY && mouseY <= ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT;

                this.isHover = isHover && !ignoreMouse;

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !ignoreMouse) {
                    if (isHover) {
                        this.isClicked = true;
                    }
                }

                if (this.isClicked && !mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.isClicked = false;

                    if (isHover && !ignoreMouse) {
                        return true;
                    }
                }

                return false;
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImage(this.isClicked ? ChessCompStompWithHacksLibrary.ChessImage.GearSelected : (this.isHover ? ChessCompStompWithHacksLibrary.ChessImage.GearHover : ChessCompStompWithHacksLibrary.ChessImage.Gear), ((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH - displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth(ChessCompStompWithHacksLibrary.ChessImage.Gear)) | 0), ((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT - displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight(ChessCompStompWithHacksLibrary.ChessImage.Gear)) | 0));
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker", {
        fields: {
            soundVolumePicker: null,
            musicVolumePicker: null
        },
        ctors: {
            ctor: function (xPos, yPos, initialSoundVolume, initialMusicVolume, elapsedMicrosPerFrame) {
                this.$initialize();
                this.soundVolumePicker = new ChessCompStompWithHacksLibrary.SoundVolumePicker(xPos, ((yPos + 50) | 0), initialSoundVolume);
                this.musicVolumePicker = new ChessCompStompWithHacksLibrary.MusicVolumePicker(xPos, yPos, initialMusicVolume);
            }
        },
        methods: {
            ProcessFrame: function (mouseInput, previousMouseInput) {
                this.soundVolumePicker.ProcessFrame(mouseInput, previousMouseInput);
                this.musicVolumePicker.ProcessFrame(mouseInput, previousMouseInput);
            },
            /**
             * Returns a number from 0 to 100 (both inclusive)
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker
             * @memberof ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker
             * @return  {number}
             */
            GetCurrentSoundVolume: function () {
                return this.soundVolumePicker.GetCurrentSoundVolume();
            },
            /**
             * Returns a number from 0 to 100 (both inclusive)
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker
             * @memberof ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker
             * @return  {number}
             */
            GetCurrentMusicVolume: function () {
                return this.musicVolumePicker.GetCurrentMusicVolume();
            },
            Render: function (displayOutput) {
                this.soundVolumePicker.Render(displayOutput);
                this.musicVolumePicker.Render(displayOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SoundVolumePicker", {
        fields: {
            _xPos: 0,
            _yPos: 0,
            _currentVolume: 0,
            _unmuteVolume: 0,
            _isDraggingVolumeSlider: false
        },
        ctors: {
            ctor: function (xPos, yPos, initialVolume) {
                this.$initialize();
                this._xPos = xPos;
                this._yPos = yPos;

                this._currentVolume = initialVolume;
                this._unmuteVolume = this._currentVolume;

                this._isDraggingVolumeSlider = false;
            }
        },
        methods: {
            ProcessFrame: function (mouseInput, previousMouseInput) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && this._xPos <= mouseX && mouseX <= ((this._xPos + 40) | 0) && this._yPos <= mouseY && mouseY <= ((this._yPos + 50) | 0)) {
                    if (this._currentVolume === 0) {
                        this._currentVolume = this._unmuteVolume === 0 ? ChessCompStompWithHacksLibrary.GlobalState.DEFAULT_VOLUME : this._unmuteVolume;
                        this._unmuteVolume = this._currentVolume;
                    } else {
                        this._unmuteVolume = this._currentVolume;
                        this._currentVolume = 0;
                    }
                }

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && ((this._xPos + 50) | 0) <= mouseX && mouseX <= ((this._xPos + 150) | 0) && ((this._yPos + 10) | 0) <= mouseY && mouseY <= ((this._yPos + 40) | 0)) {
                    this._isDraggingVolumeSlider = true;
                }

                if (this._isDraggingVolumeSlider && mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    var volume = (mouseX - (((this._xPos + 50) | 0))) | 0;
                    if (volume < 0) {
                        volume = 0;
                    }
                    if (volume > 100) {
                        volume = 100;
                    }

                    this._currentVolume = volume;
                    this._unmuteVolume = this._currentVolume;
                }

                if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this._isDraggingVolumeSlider = false;
                }
            },
            /**
             * Returns a number from 0 to 100 (both inclusive)
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacksLibrary.SoundVolumePicker
             * @memberof ChessCompStompWithHacksLibrary.SoundVolumePicker
             * @return  {number}
             */
            GetCurrentSoundVolume: function () {
                return this._currentVolume;
            },
            Render: function (displayOutput) {
                if (this._currentVolume > 0) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImage(ChessCompStompWithHacksLibrary.ChessImage.SoundOn, this._xPos, this._yPos);
                } else {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImage(ChessCompStompWithHacksLibrary.ChessImage.SoundOff, this._xPos, this._yPos);
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this._xPos + 50) | 0), ((this._yPos + 10) | 0), 101, 31, DTLibrary.DTColor.Black(), false);

                if (this._currentVolume > 0) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this._xPos + 50) | 0), ((this._yPos + 10) | 0), this._currentVolume, 31, DTLibrary.DTColor.Black(), true);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel", {
        statics: {
            fields: {
                WIDTH: 0,
                HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.WIDTH = 300;
                    this.HEIGHT = 200;
                }
            }
        },
        fields: {
            x: 0,
            y: 0,
            gameStatus: 0,
            isPlayerWhite: false,
            mouseDragXStart: null,
            mouseDragYStart: null,
            continueButton: null
        },
        ctors: {
            ctor: function (gameStatus, isPlayerWhite) {
                this.$initialize();
                this.x = 350;
                this.y = 250;
                this.gameStatus = gameStatus;
                this.isPlayerWhite = isPlayerWhite;

                this.mouseDragXStart = null;
                this.mouseDragYStart = null;

                this.continueButton = new ChessCompStompWithHacksLibrary.Button(75, 55, 150, 40, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Continue", 14, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            IsPlayerVictory: function () {
                return this.gameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory && this.isPlayerWhite || this.gameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory && !this.isPlayerWhite;
            },
            ProcessFrame: function (mouseInput, previousMouseInput) {
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                var translatedMouse = new DTLibrary.TranslatedMouse(mouseInput, ((-this.x) | 0), ((-this.y) | 0));

                var isHoverOverPanel = this.x <= mouseX && mouseX <= ((this.x + ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel.WIDTH) | 0) && this.y <= mouseY && mouseY <= ((this.y + ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel.HEIGHT) | 0);

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && isHoverOverPanel && !this.continueButton.IsHover(translatedMouse)) {
                    this.mouseDragXStart = mouseX;
                    this.mouseDragYStart = mouseY;
                }

                if (this.mouseDragXStart != null && mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.x = (this.x + (((mouseX - System.Nullable.getValue(this.mouseDragXStart)) | 0))) | 0;
                    this.y = (this.y + (((mouseY - System.Nullable.getValue(this.mouseDragYStart)) | 0))) | 0;

                    this.mouseDragXStart = mouseX;
                    this.mouseDragYStart = mouseY;
                }

                if (!mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.mouseDragXStart = null;
                    this.mouseDragYStart = null;

                    if (this.x < 0) {
                        this.x = 0;
                    }

                    if (this.y < 0) {
                        this.y = 0;
                    }

                    if (this.x > 700) {
                        this.x = 700;
                    }

                    if (this.y > 500) {
                        this.y = 500;
                    }
                }

                var isClicked = this.continueButton.ProcessFrame(translatedMouse, new DTLibrary.TranslatedMouse(previousMouseInput, ((-this.x) | 0), ((-this.y) | 0)));

                return new ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel.Result(isClicked, isHoverOverPanel || this.mouseDragXStart != null);
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, 299, 199, DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(this.x, this.y, ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel.WIDTH, ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel.HEIGHT, DTLibrary.DTColor.Black(), false);

                var text;
                var textXOffset;
                if (this.IsPlayerVictory()) {
                    text = "Victory!";
                    textXOffset = 64;
                } else if (this.gameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.Stalemate) {
                    text = "Stalemate!";
                    textXOffset = 38;
                } else if (this.gameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory || this.gameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory) {
                    text = "Defeat!";
                    textXOffset = 70;
                } else {
                    throw new System.Exception();
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((this.x + textXOffset) | 0), ((this.y + 170) | 0), text, ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                this.continueButton.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, this.x, this.y));
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel.Result", {
        $kind: "nested class",
        fields: {
            HasClickedContinueButton: false,
            IsHoverOverPanel: false
        },
        ctors: {
            ctor: function (hasClickedContinueButton, isHoverOverPanel) {
                this.$initialize();
                this.HasClickedContinueButton = hasClickedContinueButton;
                this.IsHoverOverPanel = isHoverOverPanel;
            }
        }
    });

    Bridge.define("DTLibrary.IDTLogger", {
        $kind: "interface"
    });

    Bridge.define("DTLibrary.DisplayExtensions", {
        statics: {
            methods: {
                DrawThickRectangle: function (ImageEnum, FontEnum, displayOutput, x, y, width, height, additionalThickness, color, fill) {
                    displayOutput["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle"](((x - additionalThickness) | 0), ((y - additionalThickness) | 0), ((width + Bridge.Int.mul(additionalThickness, 2)) | 0), ((1 + Bridge.Int.mul(additionalThickness, 2)) | 0), color, true);
                    displayOutput["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle"](((x - additionalThickness) | 0), ((((((height - 1) | 0) + y) | 0) - additionalThickness) | 0), ((width + Bridge.Int.mul(additionalThickness, 2)) | 0), ((1 + Bridge.Int.mul(additionalThickness, 2)) | 0), color, true);
                    displayOutput["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle"](((x - additionalThickness) | 0), ((y - additionalThickness) | 0), ((1 + Bridge.Int.mul(additionalThickness, 2)) | 0), ((height + Bridge.Int.mul(additionalThickness, 2)) | 0), color, true);
                    displayOutput["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle"](((((((width - 1) | 0) + x) | 0) - additionalThickness) | 0), ((y - additionalThickness) | 0), ((1 + Bridge.Int.mul(additionalThickness, 2)) | 0), ((height + Bridge.Int.mul(additionalThickness, 2)) | 0), color, true);

                    if (fill) {
                        displayOutput["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle"](x, y, width, height, color, true);
                    }
                }
            }
        }
    });

    /** @namespace DTLibrary */

    /**
     * Represents a color, containing the standard r, g, b, and alpha values.
     *
     * @public
     * @class DTLibrary.DTColor
     */
    Bridge.define("DTLibrary.DTColor", {
        statics: {
            methods: {
                White: function () {
                    return new DTLibrary.DTColor.ctor(255, 255, 255);
                },
                Black: function () {
                    return new DTLibrary.DTColor.ctor(0, 0, 0);
                }
            }
        },
        fields: {
            r: 0,
            g: 0,
            b: 0,
            alpha: 0
        },
        props: {
            R: {
                get: function () {
                    return this.r;
                }
            },
            G: {
                get: function () {
                    return this.g;
                }
            },
            B: {
                get: function () {
                    return this.b;
                }
            },
            Alpha: {
                get: function () {
                    return this.alpha;
                }
            }
        },
        ctors: {
            ctor: function (r, g, b) {
                this.$initialize();
                this.r = r;
                this.g = g;
                this.b = b;
                this.alpha = 255;
            },
            $ctor1: function (r, g, b, alpha) {
                this.$initialize();
                this.r = r;
                this.g = g;
                this.b = b;
                this.alpha = alpha;
            }
        }
    });

    Bridge.define("DTLibrary.DTImmutableList$1", function (T) { return {
        statics: {
            methods: {
                AsImmutableList: function (l) {
                    var immutableList = new (DTLibrary.DTImmutableList$1(T)).ctor();
                    immutableList.list = l;
                    immutableList.count = l.Count;
                    return immutableList;
                },
                EmptyList: function () {
                    return new (DTLibrary.DTImmutableList$1(T)).$ctor2(new (System.Collections.Generic.List$1(T)).ctor());
                }
            }
        },
        fields: {
            list: null,
            count: 0
        },
        props: {
            Count: {
                get: function () {
                    return this.count;
                }
            }
        },
        ctors: {
            ctor: function () {
                this.$initialize();
            },
            $ctor1: function (set) {
                var $t;
                this.$initialize();
                this.list = new (System.Collections.Generic.List$1(T)).$ctor2(set.Count);
                $t = Bridge.getEnumerator(set);
                try {
                    while ($t.moveNext()) {
                        var item = $t.Current;
                        this.list.add(item);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }
                this.count = set.Count;
            },
            $ctor2: function (list) {
                var $t;
                this.$initialize();
                this.list = new (System.Collections.Generic.List$1(T)).$ctor2(list.Count);
                $t = Bridge.getEnumerator(list);
                try {
                    while ($t.moveNext()) {
                        var item = $t.Current;
                        this.list.add(item);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }
                this.count = list.Count;
            }
        },
        methods: {
            getItem: function (index) {
                return this.list.getItem(index);
            }
        }
    }; });

    /**
     * An interface representing a (pseudo) random number generator.
     *
     * @abstract
     * @public
     * @class DTLibrary.IDTRandom
     */
    Bridge.define("DTLibrary.IDTRandom", {
        $kind: "interface"
    });

    Bridge.define("DTLibrary.GuidGenerator", {
        statics: {
            methods: {
                IntToString: function (i) {
                    switch (i) {
                        case 0: 
                            return "0";
                        case 1: 
                            return "1";
                        case 2: 
                            return "2";
                        case 3: 
                            return "3";
                        case 4: 
                            return "4";
                        case 5: 
                            return "5";
                        case 6: 
                            return "6";
                        case 7: 
                            return "7";
                        case 8: 
                            return "8";
                        case 9: 
                            return "9";
                    }

                    if (i < 0) {
                        return "-" + (DTLibrary.GuidGenerator.IntToString(((-i) | 0)) || "");
                    }

                    var x = (Bridge.Int.div(i, 10)) | 0;
                    var y = i % 10;

                    return (DTLibrary.GuidGenerator.IntToString(x) || "") + (DTLibrary.GuidGenerator.IntToString(y) || "");
                }
            }
        },
        fields: {
            currentValue1: 0,
            currentValue2: 0,
            guidString: null
        },
        ctors: {
            ctor: function (guidString) {
                this.$initialize();
                this.currentValue1 = 0;
                this.currentValue2 = 0;
                this.guidString = guidString;
            }
        },
        methods: {
            NextGuid: function () {
                if (this.currentValue1 === 2147483647) {
                    this.currentValue1 = 0;
                    this.currentValue2 = (this.currentValue2 + 1) | 0;
                } else {
                    this.currentValue1 = (this.currentValue1 + 1) | 0;
                }

                var currentValue1AsString = DTLibrary.GuidGenerator.IntToString(this.currentValue1);
                var currentValue2AsString = this.currentValue2 === 0 ? "0" : DTLibrary.GuidGenerator.IntToString(this.currentValue2);
                return "g=" + (this.guidString || "") + "," + (currentValue1AsString || "") + "," + (currentValue2AsString || "");
            }
        }
    });

    Bridge.define("DTLibrary.ITimer", {
        $kind: "interface"
    });

    Bridge.define("DTLibrary.Key", {
        $kind: "enum",
        statics: {
            fields: {
                A: 0,
                B: 1,
                C: 2,
                D: 3,
                E: 4,
                F: 5,
                G: 6,
                H: 7,
                I: 8,
                J: 9,
                K: 10,
                L: 11,
                M: 12,
                N: 13,
                O: 14,
                P: 15,
                Q: 16,
                R: 17,
                S: 18,
                T: 19,
                U: 20,
                V: 21,
                W: 22,
                X: 23,
                Y: 24,
                Z: 25,
                Zero: 26,
                One: 27,
                Two: 28,
                Three: 29,
                Four: 30,
                Five: 31,
                Six: 32,
                Seven: 33,
                Eight: 34,
                Nine: 35,
                UpArrow: 36,
                DownArrow: 37,
                LeftArrow: 38,
                RightArrow: 39,
                Delete: 40,
                Backspace: 41,
                Enter: 42,
                Shift: 43,
                Space: 44,
                Esc: 45
            }
        }
    });

    Bridge.define("DTLibrary.ListUtil", {
        statics: {
            methods: {
                Shuffle: function (T, list, random) {
                    for (var i = (list.Count - 1) | 0; i > 0; i = (i - 1) | 0) {
                        var index = random.DTLibrary$IDTRandom$NextInt(((i + 1) | 0));
                        if (index !== i) {
                            var element = list.getItem(index);
                            list.setItem(index, list.getItem(i));
                            list.setItem(i, element);
                        }
                    }
                }
            }
        }
    });

    Bridge.define("DTLibrary.StringConcatenation", {
        statics: {
            methods: {
                Concat: function (s, i) {
                    return (s || "") + (DTLibrary.StringUtil.ToStringCultureInvariant(i) || "");
                }
            }
        }
    });

    Bridge.define("DTLibrary.StringUtil", {
        statics: {
            methods: {
                StringToInt: function (str) {
                    if (Bridge.referenceEquals(str, "-2147483648")) {
                        return -2147483648;
                    }

                    if (str.charCodeAt(0) === 45) {
                        return Bridge.Int.mul(-1, DTLibrary.StringUtil.StringToInt(str.substr(1)));
                    }

                    if (str.length === 1) {
                        if (Bridge.referenceEquals(str, "0")) {
                            return 0;
                        }
                        if (Bridge.referenceEquals(str, "1")) {
                            return 1;
                        }
                        if (Bridge.referenceEquals(str, "2")) {
                            return 2;
                        }
                        if (Bridge.referenceEquals(str, "3")) {
                            return 3;
                        }
                        if (Bridge.referenceEquals(str, "4")) {
                            return 4;
                        }
                        if (Bridge.referenceEquals(str, "5")) {
                            return 5;
                        }
                        if (Bridge.referenceEquals(str, "6")) {
                            return 6;
                        }
                        if (Bridge.referenceEquals(str, "7")) {
                            return 7;
                        }
                        if (Bridge.referenceEquals(str, "8")) {
                            return 8;
                        }
                        if (Bridge.referenceEquals(str, "9")) {
                            return 9;
                        }
                    }

                    return ((DTLibrary.StringUtil.StringToInt(str.substr(((str.length - 1) | 0))) + Bridge.Int.mul(10, DTLibrary.StringUtil.StringToInt(str.substr(0, ((str.length - 1) | 0))))) | 0);
                },
                ToStringCultureInvariant: function (i) {
                    if (i === -2147483648) {
                        return "-2147483648";
                    }

                    return DTLibrary.StringUtil.IntToStringHelper(i);
                },
                IntToStringHelper: function (i) {
                    switch (i) {
                        case 0: 
                            return "0";
                        case 1: 
                            return "1";
                        case 2: 
                            return "2";
                        case 3: 
                            return "3";
                        case 4: 
                            return "4";
                        case 5: 
                            return "5";
                        case 6: 
                            return "6";
                        case 7: 
                            return "7";
                        case 8: 
                            return "8";
                        case 9: 
                            return "9";
                    }

                    if (i < 0) {
                        return "-" + (DTLibrary.StringUtil.IntToStringHelper(((-i) | 0)) || "");
                    }

                    var x = (Bridge.Int.div(i, 10)) | 0;
                    var y = i % 10;

                    return (DTLibrary.StringUtil.IntToStringHelper(x) || "") + (DTLibrary.StringUtil.IntToStringHelper(y) || "");
                }
            }
        }
    });

    Bridge.define("DTLibrary.VolumeUtil", {
        statics: {
            methods: {
                GetVolumeSmoothed: function (elapsedMicrosPerFrame, currentVolume, desiredVolume) {
                    var maxChangePerFrame = (Bridge.Int.div(elapsedMicrosPerFrame, 5000)) | 0;
                    if (maxChangePerFrame <= 0) {
                        maxChangePerFrame = 1;
                    }

                    if (Math.abs(((desiredVolume - currentVolume) | 0)) <= maxChangePerFrame) {
                        return desiredVolume;
                    } else {
                        if (desiredVolume > currentVolume) {
                            return ((currentVolume + maxChangePerFrame) | 0);
                        } else {
                            return ((currentVolume - maxChangePerFrame) | 0);
                        }
                    }
                }
            }
        }
    });

    Bridge.define("DTLibrary.DTDisplay$2", function (ImageEnum, FontEnum) { return {
        inherits: [DTLibrary.IDisplayProcessing$1(ImageEnum),DTLibrary.IDisplayOutput$2(ImageEnum,FontEnum),DTLibrary.IDisplayCleanup],
        alias: [
            "DrawImage", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawImage",
            "DrawImageRotatedClockwise$1", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawImageRotatedClockwise"
        ],
        methods: {
            DrawImage: function (image, x, y) {
                this.DrawImageRotatedClockwise(image, x, y, 0, 128);
            },
            DrawImageRotatedClockwise$1: function (image, x, y, degreesScaled) {
                this.DrawImageRotatedClockwise(image, x, y, degreesScaled, 128);
            }
        }
    }; });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessImage", {
        $kind: "enum",
        statics: {
            fields: {
                SoundOn: 0,
                SoundOff: 1,
                MusicOn: 2,
                MusicOff: 3,
                Gear: 4,
                GearHover: 5,
                GearSelected: 6,
                BlackPawn: 7,
                BlackRook: 8,
                BlackKnight: 9,
                BlackBishop: 10,
                BlackQueen: 11,
                BlackKing: 12,
                WhitePawn: 13,
                WhiteRook: 14,
                WhiteKnight: 15,
                WhiteBishop: 16,
                WhiteQueen: 17,
                WhiteKing: 18,
                Nuke_NotReady: 19,
                Nuke_Ready: 20,
                Nuke_Hover: 21,
                Nuke_Selected: 22,
                Nuke_RocketFire: 23,
                Nuke_Explosion1: 24,
                Nuke_Explosion2: 25,
                Nuke_Explosion3: 26,
                Nuke_Explosion4: 27,
                Nuke_Explosion5: 28,
                Nuke_Explosion6: 29,
                Nuke_Explosion7: 30,
                Nuke_Explosion8: 31,
                Nuke_Explosion9: 32
            }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeKeyboard", {
        inherits: [DTLibrary.IKeyboard],
        alias: ["IsPressed", "DTLibrary$IKeyboard$IsPressed"],
        ctors: {
            ctor: function () {
                this.$initialize();
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeKeyboardJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar keysBeingPressed = [];\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar mapKeyToCanonicalKey = function (key) {\r\n\t\t\t\t\t\tif (key === 'A')\r\n\t\t\t\t\t\t\treturn 'a';\r\n\t\t\t\t\t\tif (key === 'B')\r\n\t\t\t\t\t\t\treturn 'b';\r\n\t\t\t\t\t\tif (key === 'C')\r\n\t\t\t\t\t\t\treturn 'c';\r\n\t\t\t\t\t\tif (key === 'D')\r\n\t\t\t\t\t\t\treturn 'd';\r\n\t\t\t\t\t\tif (key === 'E')\r\n\t\t\t\t\t\t\treturn 'e';\r\n\t\t\t\t\t\tif (key === 'F')\r\n\t\t\t\t\t\t\treturn 'f';\r\n\t\t\t\t\t\tif (key === 'G')\r\n\t\t\t\t\t\t\treturn 'g';\r\n\t\t\t\t\t\tif (key === 'H')\r\n\t\t\t\t\t\t\treturn 'h';\r\n\t\t\t\t\t\tif (key === 'I')\r\n\t\t\t\t\t\t\treturn 'i';\r\n\t\t\t\t\t\tif (key === 'J')\r\n\t\t\t\t\t\t\treturn 'j';\r\n\t\t\t\t\t\tif (key === 'K')\r\n\t\t\t\t\t\t\treturn 'k';\r\n\t\t\t\t\t\tif (key === 'L')\r\n\t\t\t\t\t\t\treturn 'l';\r\n\t\t\t\t\t\tif (key === 'M')\r\n\t\t\t\t\t\t\treturn 'm';\r\n\t\t\t\t\t\tif (key === 'N')\r\n\t\t\t\t\t\t\treturn 'n';\r\n\t\t\t\t\t\tif (key === 'O')\r\n\t\t\t\t\t\t\treturn 'o';\r\n\t\t\t\t\t\tif (key === 'P')\r\n\t\t\t\t\t\t\treturn 'p';\r\n\t\t\t\t\t\tif (key === 'Q')\r\n\t\t\t\t\t\t\treturn 'q';\r\n\t\t\t\t\t\tif (key === 'R')\r\n\t\t\t\t\t\t\treturn 'r';\r\n\t\t\t\t\t\tif (key === 'S')\r\n\t\t\t\t\t\t\treturn 's';\r\n\t\t\t\t\t\tif (key === 'T')\r\n\t\t\t\t\t\t\treturn 't';\r\n\t\t\t\t\t\tif (key === 'U')\r\n\t\t\t\t\t\t\treturn 'u';\r\n\t\t\t\t\t\tif (key === 'V')\r\n\t\t\t\t\t\t\treturn 'v';\r\n\t\t\t\t\t\tif (key === 'W')\r\n\t\t\t\t\t\t\treturn 'w';\r\n\t\t\t\t\t\tif (key === 'X')\r\n\t\t\t\t\t\t\treturn 'x';\r\n\t\t\t\t\t\tif (key === 'Y')\r\n\t\t\t\t\t\t\treturn 'y';\r\n\t\t\t\t\t\tif (key === 'Z')\r\n\t\t\t\t\t\t\treturn 'z';\r\n\t\t\t\t\t\tif (key === '!')\r\n\t\t\t\t\t\t\treturn '1';\r\n\t\t\t\t\t\tif (key === '@')\r\n\t\t\t\t\t\t\treturn '2';\r\n\t\t\t\t\t\tif (key === '#')\r\n\t\t\t\t\t\t\treturn '3';\r\n\t\t\t\t\t\tif (key === '$')\r\n\t\t\t\t\t\t\treturn '4';\r\n\t\t\t\t\t\tif (key === '%')\r\n\t\t\t\t\t\t\treturn '5';\r\n\t\t\t\t\t\tif (key === '^')\r\n\t\t\t\t\t\t\treturn '6';\r\n\t\t\t\t\t\tif (key === '&')\r\n\t\t\t\t\t\t\treturn '7';\r\n\t\t\t\t\t\tif (key === '*')\r\n\t\t\t\t\t\t\treturn '8';\r\n\t\t\t\t\t\tif (key === '(')\r\n\t\t\t\t\t\t\treturn '9';\r\n\t\t\t\t\t\tif (key === ')')\r\n\t\t\t\t\t\t\treturn '0';\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\treturn key;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar keyDownHandler = function (e) {\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar key = mapKeyToCanonicalKey(e.key);\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < keysBeingPressed.length; i++) {\r\n\t\t\t\t\t\t\tif (keysBeingPressed[i] === key)\r\n\t\t\t\t\t\t\t\treturn;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tkeysBeingPressed.push(key);\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar keyUpHandler = function (e) {\r\n\t\t\t\t\t\tvar key = mapKeyToCanonicalKey(e.key);\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar newArray = [];\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < keysBeingPressed.length; i++) {\r\n\t\t\t\t\t\t\tif (keysBeingPressed[i] !== key)\r\n\t\t\t\t\t\t\t\tnewArray.push(keysBeingPressed[i]);\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tkeysBeingPressed = newArray;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tdocument.addEventListener('keydown', function (e) { keyDownHandler(e); }, false);\r\n\t\t\t\t\tdocument.addEventListener('keyup', function (e) { keyUpHandler(e); }, false);\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar isKeyPressed = function (k) {\r\n\t\t\t\t\t\tfor (var i = 0; i < keysBeingPressed.length; i++) {\r\n\t\t\t\t\t\t\tif (keysBeingPressed[i] === k)\r\n\t\t\t\t\t\t\t\treturn true;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\treturn false;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tisKeyPressed: isKeyPressed\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            IsPressed: function (key) {
                var correspondingKeyCode;

                switch (key) {
                    case DTLibrary.Key.A: 
                        correspondingKeyCode = "a";
                        break;
                    case DTLibrary.Key.B: 
                        correspondingKeyCode = "b";
                        break;
                    case DTLibrary.Key.C: 
                        correspondingKeyCode = "c";
                        break;
                    case DTLibrary.Key.D: 
                        correspondingKeyCode = "d";
                        break;
                    case DTLibrary.Key.E: 
                        correspondingKeyCode = "e";
                        break;
                    case DTLibrary.Key.F: 
                        correspondingKeyCode = "f";
                        break;
                    case DTLibrary.Key.G: 
                        correspondingKeyCode = "g";
                        break;
                    case DTLibrary.Key.H: 
                        correspondingKeyCode = "h";
                        break;
                    case DTLibrary.Key.I: 
                        correspondingKeyCode = "i";
                        break;
                    case DTLibrary.Key.J: 
                        correspondingKeyCode = "j";
                        break;
                    case DTLibrary.Key.K: 
                        correspondingKeyCode = "k";
                        break;
                    case DTLibrary.Key.L: 
                        correspondingKeyCode = "l";
                        break;
                    case DTLibrary.Key.M: 
                        correspondingKeyCode = "m";
                        break;
                    case DTLibrary.Key.N: 
                        correspondingKeyCode = "n";
                        break;
                    case DTLibrary.Key.O: 
                        correspondingKeyCode = "o";
                        break;
                    case DTLibrary.Key.P: 
                        correspondingKeyCode = "p";
                        break;
                    case DTLibrary.Key.Q: 
                        correspondingKeyCode = "q";
                        break;
                    case DTLibrary.Key.R: 
                        correspondingKeyCode = "r";
                        break;
                    case DTLibrary.Key.S: 
                        correspondingKeyCode = "s";
                        break;
                    case DTLibrary.Key.T: 
                        correspondingKeyCode = "t";
                        break;
                    case DTLibrary.Key.U: 
                        correspondingKeyCode = "u";
                        break;
                    case DTLibrary.Key.V: 
                        correspondingKeyCode = "v";
                        break;
                    case DTLibrary.Key.W: 
                        correspondingKeyCode = "w";
                        break;
                    case DTLibrary.Key.X: 
                        correspondingKeyCode = "x";
                        break;
                    case DTLibrary.Key.Y: 
                        correspondingKeyCode = "y";
                        break;
                    case DTLibrary.Key.Z: 
                        correspondingKeyCode = "z";
                        break;
                    case DTLibrary.Key.Zero: 
                        correspondingKeyCode = "0";
                        break;
                    case DTLibrary.Key.One: 
                        correspondingKeyCode = "1";
                        break;
                    case DTLibrary.Key.Two: 
                        correspondingKeyCode = "2";
                        break;
                    case DTLibrary.Key.Three: 
                        correspondingKeyCode = "3";
                        break;
                    case DTLibrary.Key.Four: 
                        correspondingKeyCode = "4";
                        break;
                    case DTLibrary.Key.Five: 
                        correspondingKeyCode = "5";
                        break;
                    case DTLibrary.Key.Six: 
                        correspondingKeyCode = "6";
                        break;
                    case DTLibrary.Key.Seven: 
                        correspondingKeyCode = "7";
                        break;
                    case DTLibrary.Key.Eight: 
                        correspondingKeyCode = "8";
                        break;
                    case DTLibrary.Key.Nine: 
                        correspondingKeyCode = "9";
                        break;
                    case DTLibrary.Key.UpArrow: 
                        correspondingKeyCode = "ArrowUp";
                        break;
                    case DTLibrary.Key.DownArrow: 
                        correspondingKeyCode = "ArrowDown";
                        break;
                    case DTLibrary.Key.LeftArrow: 
                        correspondingKeyCode = "ArrowLeft";
                        break;
                    case DTLibrary.Key.RightArrow: 
                        correspondingKeyCode = "ArrowRight";
                        break;
                    case DTLibrary.Key.Delete: 
                        correspondingKeyCode = "Delete";
                        break;
                    case DTLibrary.Key.Backspace: 
                        correspondingKeyCode = "Backspace";
                        break;
                    case DTLibrary.Key.Enter: 
                        correspondingKeyCode = "Enter";
                        break;
                    case DTLibrary.Key.Shift: 
                        correspondingKeyCode = "Shift";
                        break;
                    case DTLibrary.Key.Space: 
                        correspondingKeyCode = " ";
                        break;
                    case DTLibrary.Key.Esc: 
                        correspondingKeyCode = "Escape";
                        break;
                    default: 
                        throw new System.Exception();
                }

                /* 
                				None of the keycodes need to be escaped
                				(but this would be necessary if we had a key
                				such as backslash)
                			*/
                var javascriptCode = "window.ChessCompStompWithHacksBridgeKeyboardJavascript.isKeyPressed('" + (correspondingKeyCode || "") + "')";

                var result = eval(javascriptCode);

                if (result) {
                    return true;
                }

                return false;

            }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeMouse", {
        inherits: [DTLibrary.IMouse],
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function () {
                this.$initialize();
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeMouseJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar mouseXPosition = 0;\r\n\t\t\t\t\tvar mouseYPosition = 0;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar canvas = null;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar mouseMoveHandler = function (e) {\r\n\t\t\t\t\t\r\n\t\t\t\t\t\tif (canvas === null) {\r\n\t\t\t\t\t\t\tcanvas = document.getElementById('chessCompStompWithHacksCanvas');\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (canvas === null)\r\n\t\t\t\t\t\t\t\treturn;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar xPosition = (e.pageX !== null && e.pageX !== undefined ? e.pageX : e.clientX) - canvas.offsetLeft;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (xPosition < 0)\r\n\t\t\t\t\t\t\txPosition = 0;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (xPosition > canvas.width)\r\n\t\t\t\t\t\t\txPosition = canvas.width;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar yPosition = (e.pageY !== null && e.pageY !== undefined ? e.pageY : e.clientY) - canvas.offsetTop;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (yPosition < 0)\r\n\t\t\t\t\t\t\tyPosition = 0;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (yPosition > canvas.height)\r\n\t\t\t\t\t\t\tyPosition = canvas.height;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tmouseXPosition = xPosition;\r\n\t\t\t\t\t\tmouseYPosition = canvas.height - yPosition - 1;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar isLeftMouseButtonPressed = false;\r\n\t\t\t\t\tvar isRightMouseButtonPressed = false;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar checkMouseButtonHandler = function (e) {\r\n\t\t\t\t\t\tif ((e.buttons & 1) === 1)\r\n\t\t\t\t\t\t\tisLeftMouseButtonPressed = true;\r\n\t\t\t\t\t\telse\r\n\t\t\t\t\t\t\tisLeftMouseButtonPressed = false;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif ((e.buttons & 2) === 2)\r\n\t\t\t\t\t\t\tisRightMouseButtonPressed = true;\r\n\t\t\t\t\t\telse\r\n\t\t\t\t\t\t\tisRightMouseButtonPressed = false;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tdocument.addEventListener('mousemove', function (e) { mouseMoveHandler(e); checkMouseButtonHandler(e); }, false);\r\n\t\t\t\t\tdocument.addEventListener('mousedown', function (e) { checkMouseButtonHandler(e); }, false);\r\n\t\t\t\t\tdocument.addEventListener('mouseup', function (e) { checkMouseButtonHandler(e); }, false);\r\n\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tisLeftMouseButtonPressed: function () { return isLeftMouseButtonPressed; },\r\n\t\t\t\t\t\tisRightMouseButtonPressed: function () { return isRightMouseButtonPressed; },\r\n\t\t\t\t\t\tgetMouseX: function () { return Math.round(mouseXPosition); },\r\n\t\t\t\t\t\tgetMouseY: function () { return Math.round(mouseYPosition); }\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            GetX: function () {
                return window.ChessCompStompWithHacksBridgeMouseJavascript.getMouseX();
            },
            GetY: function () {
                return window.ChessCompStompWithHacksBridgeMouseJavascript.getMouseY();
            },
            IsLeftMouseButtonPressed: function () {
                return window.ChessCompStompWithHacksBridgeMouseJavascript.isLeftMouseButtonPressed();
            },
            IsRightMouseButtonPressed: function () {
                return window.ChessCompStompWithHacksBridgeMouseJavascript.isRightMouseButtonPressed();
            }
        }
    });

    Bridge.definei("DTLibrary.IMusic$1", function (MusicEnum) { return {
        inherits: [DTLibrary.IMusicOutput$1(MusicEnum),DTLibrary.IMusicProcessing,DTLibrary.IMusicCleanup],
        $kind: "interface"
    }; });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessMusic", {
        $kind: "enum"
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessSound", {
        $kind: "enum"
    });

    Bridge.define("ChessCompStompWithHacksEngine.AlphaBetaAI", {
        inherits: [ChessCompStompWithHacksEngine.IChessAI],
        statics: {
            methods: {
                GetDepthOfNextSearch: function (depthOfBestMoveFoundSoFar) {
                    var returnValue = (depthOfBestMoveFoundSoFar + 2) | 0;

                    if (returnValue > 50) {
                        returnValue = 50;
                    }

                    return returnValue;
                }
            }
        },
        fields: {
            startTimeMicroSeconds: System.Int64(0),
            originalGameState: null,
            gameStateWithNoNuke: null,
            timer: null,
            random: null,
            logger: null,
            boardEvaluator: null,
            alphaBetaProcess: null,
            bestMoveFoundSoFar: null,
            depthOfBestMoveFoundSoFar: null,
            topLevelMoves: null
        },
        alias: [
            "GetStartTimeMicroSeconds", "ChessCompStompWithHacksEngine$IChessAI$GetStartTimeMicroSeconds",
            "GetBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetBestMoveFoundSoFar",
            "GetDepthOfBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetDepthOfBestMoveFoundSoFar",
            "HasFinishedCalculation", "ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation",
            "CalculateBestMove", "ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove"
        ],
        ctors: {
            ctor: function (gameState, timer, random, logger) {
                this.$initialize();
                this.startTimeMicroSeconds = timer.DTLibrary$ITimer$GetNumberOfMicroSeconds();

                this.originalGameState = gameState;
                this.gameStateWithNoNuke = gameState.IsPlayerTurn() && gameState.Abilities.HasTacticalNuke && gameState.HasUsedNuke === false ? gameState : ChessCompStompWithHacksEngine.GameStateUtil.GetGameStateWithoutNukeAbility(gameState);
                this.timer = timer;
                this.random = random;
                this.logger = logger;
                this.boardEvaluator = new ChessCompStompWithHacksEngine.StandardBoardEvaluator(random);

                var result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(gameState);

                if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.InProgress) {
                    this.alphaBetaProcess = null;
                    this.bestMoveFoundSoFar = result.Moves.getItem(random.DTLibrary$IDTRandom$NextInt(result.Moves.Count));
                    this.depthOfBestMoveFoundSoFar = 0;

                    var moves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Move)).$ctor1(result.Moves);
                    DTLibrary.ListUtil.Shuffle(ChessCompStompWithHacksEngine.Move, moves, random);
                    this.topLevelMoves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Move)).ctor();
                    var nonPawnAndCapturingMoves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Move)).ctor();
                    for (var i = 0; i < moves.Count; i = (i + 1) | 0) {
                        if (ChessCompStompWithHacksEngine.MoveUtil.IsPawnMove(moves.getItem(i), gameState.Board) || ChessCompStompWithHacksEngine.MoveUtil.IsCapturingMove(moves.getItem(i), gameState.Board)) {
                            this.topLevelMoves.add(moves.getItem(i));
                        } else {
                            nonPawnAndCapturingMoves.add(moves.getItem(i));
                        }
                    }
                    this.topLevelMoves.AddRange(nonPawnAndCapturingMoves);
                } else {
                    this.alphaBetaProcess = null;
                    this.bestMoveFoundSoFar = null;
                    this.depthOfBestMoveFoundSoFar = null;

                    this.topLevelMoves = null;
                }
            }
        },
        methods: {
            GetStartTimeMicroSeconds: function () {
                return this.startTimeMicroSeconds;
            },
            GetBestMoveFoundSoFar: function () {
                if (this.bestMoveFoundSoFar == null) {
                    throw new System.Exception();
                }
                return this.bestMoveFoundSoFar;
            },
            GetDepthOfBestMoveFoundSoFar: function () {
                if (this.depthOfBestMoveFoundSoFar == null) {
                    throw new System.Exception();
                }
                return System.Nullable.getValue(this.depthOfBestMoveFoundSoFar);
            },
            HasFinishedCalculation: function () {
                return false;
            },
            CalculateBestMove: function (millisecondsToThink) {
                if (this.bestMoveFoundSoFar == null) {
                    return;
                }

                var count = 0;
                var startingTimeMillis = this.timer.DTLibrary$ITimer$GetNumberOfMicroSeconds().div(System.Int64(1000));
                while (true) {
                    count = (count + 1) | 0;
                    if (count === 5) {
                        count = 0;
                        var currentTimeMillis = this.timer.DTLibrary$ITimer$GetNumberOfMicroSeconds().div(System.Int64(1000));
                        var elapsedTimeAsLong = currentTimeMillis.sub(startingTimeMillis);
                        var elapsedTimeMillis = System.Int64.clip32(elapsedTimeAsLong);

                        if (elapsedTimeMillis >= millisecondsToThink) {
                            return;
                        }
                    }

                    if (this.alphaBetaProcess == null) {
                        this.alphaBetaProcess = Bridge.getEnumerator(this.BeginAlphaBeta(this.gameStateWithNoNuke, ChessCompStompWithHacksEngine.AlphaBetaAI.GetDepthOfNextSearch(System.Nullable.getValue(this.depthOfBestMoveFoundSoFar))));
                    } else {
                        this.alphaBetaProcess.System$Collections$IEnumerator$moveNext();
                        var value = this.alphaBetaProcess.System$Collections$IEnumerator$Current;
                        if (value != null) {
                            this.bestMoveFoundSoFar = Bridge.cast(value, ChessCompStompWithHacksEngine.Move);
                            this.depthOfBestMoveFoundSoFar = ChessCompStompWithHacksEngine.AlphaBetaAI.GetDepthOfNextSearch(System.Nullable.getValue(this.depthOfBestMoveFoundSoFar));
                            this.alphaBetaProcess = null;
                        }
                    }
                }
            },
            BeginAlphaBeta: function (gameState, depth) {
                return new Bridge.GeneratorEnumerable(Bridge.fn.bind(this, function (gameState, depth) {
                    var $step = 0,
                        $jumpFromFinally,
                        $returnValue,
                        best,
                        bestMove,
                        alpha,
                        i,
                        move,
                        invoke,
                        temp,
                        $t,
                        x,
                        moveValue,
                        $async_e;

                    var $enumerator = new Bridge.GeneratorEnumerator(Bridge.fn.bind(this, function () {
                        try {
                            for (;;) {
                                switch ($step) {
                                    case 0: {
                                        best = null;
                                            bestMove = null;
                                            alpha = null;
                                            i = 0;
                                            $step = 1;
                                            continue;
                                    }
                                    case 1: {
                                        if ( i < this.topLevelMoves.Count ) {
                                                $step = 2;
                                                continue;
                                            }
                                        $step = 8;
                                        continue;
                                    }
                                    case 2: {
                                        move = this.topLevelMoves.getItem(i);
                                            invoke = this.AlphaBetaHelper(ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, move), ((depth - 1) | 0), alpha, null, false);

                                            temp = null;
                                            $t = Bridge.getEnumerator(invoke);
                                            $step = 3;
                                            continue;
                                    }
                                    case 3: {
                                        if ($t.moveNext()) {
                                                x = $t.Current;
                                                $step = 4;
                                                continue;
                                            }
                                        $step = 6;
                                        continue;
                                    }
                                    case 4: {
                                        $enumerator.current = null;
                                            $step = 5;
                                            return true;
                                    }
                                    case 5: {
                                        if (x != null) {
                                                temp = Bridge.cast(Bridge.unbox(x, System.Int32), System.Int32, true);
                                            }
                                        $step = 3;
                                        continue;
                                    }
                                    case 6: {
                                        if (temp == null) {
                                                throw new System.Exception();
                                            }
                                            moveValue = System.Nullable.getValue(temp);

                                            if (best == null || moveValue > System.Nullable.getValue(best)) {
                                                best = moveValue;
                                                bestMove = move;
                                            }

                                            if (alpha == null || System.Nullable.getValue(best) >= System.Nullable.getValue(alpha)) {
                                                alpha = System.Nullable.getValue(best);
                                            }
                                        $step = 7;
                                        continue;
                                    }
                                    case 7: {
                                        i = (i + 1) | 0;
                                        $step = 1;
                                        continue;
                                    }
                                    case 8: {
                                        if (bestMove == null) {
                                                throw new System.Exception();
                                            }

                                            this.logger.DTLibrary$IDTLogger$WriteLine$1("Found best move at depth " + (DTLibrary.StringUtil.ToStringCultureInvariant(depth) || "") + " with score: " + ((gameState.IsWhiteTurn ? DTLibrary.StringUtil.ToStringCultureInvariant(System.Nullable.getValue(best)) : DTLibrary.StringUtil.ToStringCultureInvariant((((-System.Nullable.getValue(best)) | 0)))) || ""));
                                            $enumerator.current = bestMove;
                                            $step = 9;
                                            return true;
                                    }
                                    case 9: {
                                        return false;
                                    }
                                    default: {
                                        return false;
                                    }
                                }
                            }
                        } catch($async_e1) {
                            $async_e = System.Exception.create($async_e1);
                            throw $async_e;
                        }
                    }));
                    return $enumerator;
                }, arguments));
            },
            AlphaBetaHelper: function (gameState, depth, alpha, beta, isCurrentPlayer) {
                return new Bridge.GeneratorEnumerable(Bridge.fn.bind(this, function (gameState, depth, alpha, beta, isCurrentPlayer) {
                    var $step = 0,
                        $jumpFromFinally,
                        $returnValue,
                        returnValue,
                        result,
                        returnValue1,
                        returnValue2,
                        returnValue3,
                        best,
                        i,
                        move,
                        invoke,
                        temp,
                        $t,
                        x,
                        moveValue,
                        best1,
                        i1,
                        move1,
                        invoke1,
                        temp1,
                        $t1,
                        x1,
                        moveValue1,
                        $async_e;

                    var $enumerator = new Bridge.GeneratorEnumerator(Bridge.fn.bind(this, function () {
                        try {
                            for (;;) {
                                switch ($step) {
                                    case 0: {
                                        if (depth === 0) {
                                                $step = 1;
                                                continue;
                                            } 
                                            $step = 3;
                                            continue;
                                    }
                                    case 1: {
                                        returnValue = this.boardEvaluator.ChessCompStompWithHacksEngine$IBoardEvaluator$Evaluate(gameState, this.originalGameState.IsWhiteTurn);
                                            $enumerator.current = Bridge.box(returnValue, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 2;
                                            return true;
                                    }
                                    case 2: {
                                        return false;
                                    }
                                    case 3: {
                                        result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(gameState);

                                            if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.Stalemate) {
                                                $step = 4;
                                                continue;
                                            } 
                                            $step = 6;
                                            continue;
                                    }
                                    case 4: {
                                        returnValue1 = 0;
                                            $enumerator.current = Bridge.box(returnValue1, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 5;
                                            return true;
                                    }
                                    case 5: {
                                        return false;
                                    }
                                    case 6: {
                                        if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory) {
                                                $step = 7;
                                                continue;
                                            } 
                                            $step = 9;
                                            continue;
                                    }
                                    case 7: {
                                        if (this.originalGameState.IsWhiteTurn) {
                                                returnValue2 = Bridge.Int.clip32(2147482647 + depth);
                                            } else {
                                                returnValue2 = Bridge.Int.clip32(-2147482648 - depth);
                                            }
                                            $enumerator.current = Bridge.box(returnValue2, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 8;
                                            return true;
                                    }
                                    case 8: {
                                        return false;
                                    }
                                    case 9: {
                                        if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory) {
                                                $step = 10;
                                                continue;
                                            } 
                                            $step = 12;
                                            continue;
                                    }
                                    case 10: {
                                        if (this.originalGameState.IsWhiteTurn) {
                                                returnValue3 = Bridge.Int.clip32(-2147482648 - depth);
                                            } else {
                                                returnValue3 = Bridge.Int.clip32(2147482647 + depth);
                                            }
                                            $enumerator.current = Bridge.box(returnValue3, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 11;
                                            return true;
                                    }
                                    case 11: {
                                        return false;
                                    }
                                    case 12: {
                                        if (isCurrentPlayer) {
                                                $step = 13;
                                                continue;
                                            } else  {
                                                $step = 23;
                                                continue;
                                            }
                                    }
                                    case 13: {
                                        best = null;
                                            i = 0;
                                            $step = 14;
                                            continue;
                                    }
                                    case 14: {
                                        if ( i < result.Moves.Count ) {
                                                $step = 15;
                                                continue;
                                            }
                                        $step = 21;
                                        continue;
                                    }
                                    case 15: {
                                        move = result.Moves.getItem(i);

                                            invoke = this.AlphaBetaHelper(ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, move), ((depth - 1) | 0), alpha, beta, false);

                                            temp = null;
                                            $t = Bridge.getEnumerator(invoke);
                                            $step = 16;
                                            continue;
                                    }
                                    case 16: {
                                        if ($t.moveNext()) {
                                                x = $t.Current;
                                                $step = 17;
                                                continue;
                                            }
                                        $step = 19;
                                        continue;
                                    }
                                    case 17: {
                                        $enumerator.current = null;
                                            $step = 18;
                                            return true;
                                    }
                                    case 18: {
                                        if (x != null) {
                                                temp = Bridge.cast(Bridge.unbox(x, System.Int32), System.Int32, true);
                                            }
                                        $step = 16;
                                        continue;
                                    }
                                    case 19: {
                                        if (temp == null) {
                                                throw new System.Exception();
                                            }
                                            moveValue = System.Nullable.getValue(temp);

                                            if (best == null || moveValue >= System.Nullable.getValue(best)) {
                                                best = moveValue;
                                            }

                                            if (alpha == null || System.Nullable.getValue(best) >= System.Nullable.getValue(alpha)) {
                                                alpha = System.Nullable.getValue(best);
                                            }

                                            if (System.Nullable.hasValue(alpha) && System.Nullable.hasValue(beta) && System.Nullable.getValue(alpha) >= System.Nullable.getValue(beta)) {
                                                $step = 21;
                                                continue;
                                            }
                                        $step = 20;
                                        continue;
                                    }
                                    case 20: {
                                        i = (i + 1) | 0;
                                        $step = 14;
                                        continue;
                                    }
                                    case 21: {
                                        if (best == null) {
                                                throw new System.Exception();
                                            }

                                            $enumerator.current = Bridge.box(best, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 22;
                                            return true;
                                    }
                                    case 22: {
                                        return false;
                                    }
                                    case 23: {
                                        best1 = null;
                                            i1 = 0;
                                            $step = 24;
                                            continue;
                                    }
                                    case 24: {
                                        if ( i1 < result.Moves.Count ) {
                                                $step = 25;
                                                continue;
                                            }
                                        $step = 31;
                                        continue;
                                    }
                                    case 25: {
                                        move1 = result.Moves.getItem(i1);

                                            invoke1 = this.AlphaBetaHelper(ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, move1), ((depth - 1) | 0), alpha, beta, true);

                                            temp1 = null;
                                            $t1 = Bridge.getEnumerator(invoke1);
                                            $step = 26;
                                            continue;
                                    }
                                    case 26: {
                                        if ($t1.moveNext()) {
                                                x1 = $t1.Current;
                                                $step = 27;
                                                continue;
                                            }
                                        $step = 29;
                                        continue;
                                    }
                                    case 27: {
                                        $enumerator.current = null;
                                            $step = 28;
                                            return true;
                                    }
                                    case 28: {
                                        if (x1 != null) {
                                                temp1 = Bridge.cast(Bridge.unbox(x1, System.Int32), System.Int32, true);
                                            }
                                        $step = 26;
                                        continue;
                                    }
                                    case 29: {
                                        if (temp1 == null) {
                                                throw new System.Exception();
                                            }
                                            moveValue1 = System.Nullable.getValue(temp1);

                                            if (best1 == null || moveValue1 <= System.Nullable.getValue(best1)) {
                                                best1 = moveValue1;
                                            }

                                            if (beta == null || System.Nullable.getValue(best1) <= System.Nullable.getValue(beta)) {
                                                beta = System.Nullable.getValue(best1);
                                            }

                                            if (System.Nullable.hasValue(alpha) && System.Nullable.hasValue(beta) && System.Nullable.getValue(beta) <= System.Nullable.getValue(alpha)) {
                                                $step = 31;
                                                continue;
                                            }
                                        $step = 30;
                                        continue;
                                    }
                                    case 30: {
                                        i1 = (i1 + 1) | 0;
                                        $step = 24;
                                        continue;
                                    }
                                    case 31: {
                                        if (best1 == null) {
                                                throw new System.Exception();
                                            }
                                            $enumerator.current = Bridge.box(best1, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 32;
                                            return true;
                                    }
                                    case 32: {
                                        return false;
                                    }
                                    case 33: {

                                    }
                                    default: {
                                        return false;
                                    }
                                }
                            }
                        } catch($async_e1) {
                            $async_e = System.Exception.create($async_e1);
                            throw $async_e;
                        }
                    }));
                    return $enumerator;
                }, arguments));
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.CompositeAI", {
        inherits: [ChessCompStompWithHacksEngine.IChessAI],
        fields: {
            startTimeMicroSeconds: System.Int64(0),
            underlyingAI: null
        },
        alias: [
            "GetStartTimeMicroSeconds", "ChessCompStompWithHacksEngine$IChessAI$GetStartTimeMicroSeconds",
            "GetBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetBestMoveFoundSoFar",
            "GetDepthOfBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetDepthOfBestMoveFoundSoFar",
            "HasFinishedCalculation", "ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation",
            "CalculateBestMove", "ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove"
        ],
        ctors: {
            ctor: function (gameState, timer, random, logger) {
                this.$initialize();
                this.startTimeMicroSeconds = timer.DTLibrary$ITimer$GetNumberOfMicroSeconds();

                if (gameState.TurnCount <= 4) {
                    this.underlyingAI = new ChessCompStompWithHacksEngine.RandomMoveAI(gameState, timer, random);
                } else {
                    if (gameState.TurnCount <= 20) {
                        this.underlyingAI = new ChessCompStompWithHacksEngine.EarlyGameAI(gameState, timer, random, logger);
                    } else {
                        this.underlyingAI = new ChessCompStompWithHacksEngine.AlphaBetaAI(gameState, timer, random, logger);
                    }
                }
            }
        },
        methods: {
            GetStartTimeMicroSeconds: function () {
                return this.startTimeMicroSeconds;
            },
            GetBestMoveFoundSoFar: function () {
                return this.underlyingAI.ChessCompStompWithHacksEngine$IChessAI$GetBestMoveFoundSoFar();
            },
            GetDepthOfBestMoveFoundSoFar: function () {
                return this.underlyingAI.ChessCompStompWithHacksEngine$IChessAI$GetDepthOfBestMoveFoundSoFar();
            },
            HasFinishedCalculation: function () {
                return this.underlyingAI.ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation();
            },
            CalculateBestMove: function (millisecondsToThink) {
                this.underlyingAI.ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove(millisecondsToThink);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.EarlyGameAI", {
        inherits: [ChessCompStompWithHacksEngine.IChessAI],
        statics: {
            methods: {
                GetDepthOfNextSearch: function (depthOfBestMoveFoundSoFar) {
                    var returnValue = (depthOfBestMoveFoundSoFar + 2) | 0;

                    if (returnValue > 50) {
                        returnValue = 50;
                    }

                    return returnValue;
                }
            }
        },
        fields: {
            startTimeMicroSeconds: System.Int64(0),
            originalGameState: null,
            gameStateWithNoNuke: null,
            timer: null,
            random: null,
            logger: null,
            boardEvaluator: null,
            alphaBetaProcess: null,
            bestMoveFoundSoFar: null,
            depthOfBestMoveFoundSoFar: null,
            topLevelMoves: null
        },
        alias: [
            "GetStartTimeMicroSeconds", "ChessCompStompWithHacksEngine$IChessAI$GetStartTimeMicroSeconds",
            "GetBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetBestMoveFoundSoFar",
            "GetDepthOfBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetDepthOfBestMoveFoundSoFar",
            "HasFinishedCalculation", "ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation",
            "CalculateBestMove", "ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove"
        ],
        ctors: {
            ctor: function (gameState, timer, random, logger) {
                this.$initialize();
                this.startTimeMicroSeconds = timer.DTLibrary$ITimer$GetNumberOfMicroSeconds();

                this.originalGameState = gameState;
                this.gameStateWithNoNuke = gameState.IsPlayerTurn() && gameState.Abilities.HasTacticalNuke && gameState.HasUsedNuke === false ? gameState : ChessCompStompWithHacksEngine.GameStateUtil.GetGameStateWithoutNukeAbility(gameState);
                this.timer = timer;
                this.random = random;
                this.logger = logger;
                this.boardEvaluator = new ChessCompStompWithHacksEngine.RandomizedBoardEvaluator(random);

                var result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(gameState);

                if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.InProgress) {
                    this.alphaBetaProcess = null;
                    this.bestMoveFoundSoFar = result.Moves.getItem(random.DTLibrary$IDTRandom$NextInt(result.Moves.Count));
                    this.depthOfBestMoveFoundSoFar = 0;

                    var moves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Move)).$ctor1(result.Moves);
                    DTLibrary.ListUtil.Shuffle(ChessCompStompWithHacksEngine.Move, moves, random);
                    var numberOfMovesToKeep = Math.max(1, ((Bridge.Int.div(Bridge.Int.mul(moves.Count, 3), 4)) | 0));
                    this.topLevelMoves = new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Move)).ctor();
                    for (var i = 0; i < numberOfMovesToKeep; i = (i + 1) | 0) {
                        this.topLevelMoves.add(moves.getItem(i));
                    }
                } else {
                    this.alphaBetaProcess = null;
                    this.bestMoveFoundSoFar = null;
                    this.depthOfBestMoveFoundSoFar = null;

                    this.topLevelMoves = null;
                }
            }
        },
        methods: {
            GetStartTimeMicroSeconds: function () {
                return this.startTimeMicroSeconds;
            },
            GetBestMoveFoundSoFar: function () {
                if (this.bestMoveFoundSoFar == null) {
                    throw new System.Exception();
                }
                return this.bestMoveFoundSoFar;
            },
            GetDepthOfBestMoveFoundSoFar: function () {
                if (this.depthOfBestMoveFoundSoFar == null) {
                    throw new System.Exception();
                }
                return System.Nullable.getValue(this.depthOfBestMoveFoundSoFar);
            },
            HasFinishedCalculation: function () {
                return false;
            },
            CalculateBestMove: function (millisecondsToThink) {
                if (this.bestMoveFoundSoFar == null) {
                    return;
                }

                var count = 0;
                var startingTimeMillis = this.timer.DTLibrary$ITimer$GetNumberOfMicroSeconds().div(System.Int64(1000));
                while (true) {
                    count = (count + 1) | 0;
                    if (count === 5) {
                        count = 0;
                        var currentTimeMillis = this.timer.DTLibrary$ITimer$GetNumberOfMicroSeconds().div(System.Int64(1000));
                        var elapsedTimeAsLong = currentTimeMillis.sub(startingTimeMillis);
                        var elapsedTimeMillis = System.Int64.clip32(elapsedTimeAsLong);

                        if (elapsedTimeMillis >= millisecondsToThink) {
                            return;
                        }
                    }

                    if (this.alphaBetaProcess == null) {
                        this.alphaBetaProcess = Bridge.getEnumerator(this.BeginAlphaBeta(this.gameStateWithNoNuke, ChessCompStompWithHacksEngine.EarlyGameAI.GetDepthOfNextSearch(System.Nullable.getValue(this.depthOfBestMoveFoundSoFar))));
                    } else {
                        this.alphaBetaProcess.System$Collections$IEnumerator$moveNext();
                        var value = this.alphaBetaProcess.System$Collections$IEnumerator$Current;
                        if (value != null) {
                            this.bestMoveFoundSoFar = Bridge.cast(value, ChessCompStompWithHacksEngine.Move);
                            this.depthOfBestMoveFoundSoFar = ChessCompStompWithHacksEngine.EarlyGameAI.GetDepthOfNextSearch(System.Nullable.getValue(this.depthOfBestMoveFoundSoFar));
                            this.alphaBetaProcess = null;
                        }
                    }
                }
            },
            BeginAlphaBeta: function (gameState, depth) {
                return new Bridge.GeneratorEnumerable(Bridge.fn.bind(this, function (gameState, depth) {
                    var $step = 0,
                        $jumpFromFinally,
                        $returnValue,
                        best,
                        bestMove,
                        alpha,
                        i,
                        move,
                        invoke,
                        temp,
                        $t,
                        x,
                        moveValue,
                        $async_e;

                    var $enumerator = new Bridge.GeneratorEnumerator(Bridge.fn.bind(this, function () {
                        try {
                            for (;;) {
                                switch ($step) {
                                    case 0: {
                                        best = null;
                                            bestMove = null;
                                            alpha = null;
                                            i = 0;
                                            $step = 1;
                                            continue;
                                    }
                                    case 1: {
                                        if ( i < this.topLevelMoves.Count ) {
                                                $step = 2;
                                                continue;
                                            }
                                        $step = 8;
                                        continue;
                                    }
                                    case 2: {
                                        move = this.topLevelMoves.getItem(i);
                                            invoke = this.AlphaBetaHelper(ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, move), ((depth - 1) | 0), alpha, null, false);

                                            temp = null;
                                            $t = Bridge.getEnumerator(invoke);
                                            $step = 3;
                                            continue;
                                    }
                                    case 3: {
                                        if ($t.moveNext()) {
                                                x = $t.Current;
                                                $step = 4;
                                                continue;
                                            }
                                        $step = 6;
                                        continue;
                                    }
                                    case 4: {
                                        $enumerator.current = null;
                                            $step = 5;
                                            return true;
                                    }
                                    case 5: {
                                        if (x != null) {
                                                temp = Bridge.cast(Bridge.unbox(x, System.Int32), System.Int32, true);
                                            }
                                        $step = 3;
                                        continue;
                                    }
                                    case 6: {
                                        if (temp == null) {
                                                throw new System.Exception();
                                            }
                                            moveValue = System.Nullable.getValue(temp);

                                            if (best == null || moveValue > System.Nullable.getValue(best)) {
                                                best = moveValue;
                                                bestMove = move;
                                            }

                                            if (alpha == null || System.Nullable.getValue(best) >= System.Nullable.getValue(alpha)) {
                                                alpha = System.Nullable.getValue(best);
                                            }
                                        $step = 7;
                                        continue;
                                    }
                                    case 7: {
                                        i = (i + 1) | 0;
                                        $step = 1;
                                        continue;
                                    }
                                    case 8: {
                                        if (bestMove == null) {
                                                throw new System.Exception();
                                            }

                                            this.logger.DTLibrary$IDTLogger$WriteLine$1("Found best move at depth " + (DTLibrary.StringUtil.ToStringCultureInvariant(depth) || "") + " with score: " + ((gameState.IsWhiteTurn ? DTLibrary.StringUtil.ToStringCultureInvariant(System.Nullable.getValue(best)) : DTLibrary.StringUtil.ToStringCultureInvariant((((-System.Nullable.getValue(best)) | 0)))) || ""));
                                            $enumerator.current = bestMove;
                                            $step = 9;
                                            return true;
                                    }
                                    case 9: {
                                        return false;
                                    }
                                    default: {
                                        return false;
                                    }
                                }
                            }
                        } catch($async_e1) {
                            $async_e = System.Exception.create($async_e1);
                            throw $async_e;
                        }
                    }));
                    return $enumerator;
                }, arguments));
            },
            AlphaBetaHelper: function (gameState, depth, alpha, beta, isCurrentPlayer) {
                return new Bridge.GeneratorEnumerable(Bridge.fn.bind(this, function (gameState, depth, alpha, beta, isCurrentPlayer) {
                    var $step = 0,
                        $jumpFromFinally,
                        $returnValue,
                        returnValue,
                        result,
                        returnValue1,
                        returnValue2,
                        returnValue3,
                        best,
                        i,
                        move,
                        invoke,
                        temp,
                        $t,
                        x,
                        moveValue,
                        best1,
                        i1,
                        move1,
                        invoke1,
                        temp1,
                        $t1,
                        x1,
                        moveValue1,
                        $async_e;

                    var $enumerator = new Bridge.GeneratorEnumerator(Bridge.fn.bind(this, function () {
                        try {
                            for (;;) {
                                switch ($step) {
                                    case 0: {
                                        if (depth === 0) {
                                                $step = 1;
                                                continue;
                                            } 
                                            $step = 3;
                                            continue;
                                    }
                                    case 1: {
                                        returnValue = this.boardEvaluator.ChessCompStompWithHacksEngine$IBoardEvaluator$Evaluate(gameState, this.originalGameState.IsWhiteTurn);
                                            $enumerator.current = Bridge.box(returnValue, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 2;
                                            return true;
                                    }
                                    case 2: {
                                        return false;
                                    }
                                    case 3: {
                                        result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(gameState);

                                            if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.Stalemate) {
                                                $step = 4;
                                                continue;
                                            } 
                                            $step = 6;
                                            continue;
                                    }
                                    case 4: {
                                        returnValue1 = 0;
                                            $enumerator.current = Bridge.box(returnValue1, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 5;
                                            return true;
                                    }
                                    case 5: {
                                        return false;
                                    }
                                    case 6: {
                                        if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory) {
                                                $step = 7;
                                                continue;
                                            } 
                                            $step = 9;
                                            continue;
                                    }
                                    case 7: {
                                        if (this.originalGameState.IsWhiteTurn) {
                                                returnValue2 = Bridge.Int.clip32(2147482647 + depth);
                                            } else {
                                                returnValue2 = Bridge.Int.clip32(-2147482648 - depth);
                                            }
                                            $enumerator.current = Bridge.box(returnValue2, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 8;
                                            return true;
                                    }
                                    case 8: {
                                        return false;
                                    }
                                    case 9: {
                                        if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory) {
                                                $step = 10;
                                                continue;
                                            } 
                                            $step = 12;
                                            continue;
                                    }
                                    case 10: {
                                        if (this.originalGameState.IsWhiteTurn) {
                                                returnValue3 = Bridge.Int.clip32(-2147482648 - depth);
                                            } else {
                                                returnValue3 = Bridge.Int.clip32(2147482647 + depth);
                                            }
                                            $enumerator.current = Bridge.box(returnValue3, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 11;
                                            return true;
                                    }
                                    case 11: {
                                        return false;
                                    }
                                    case 12: {
                                        if (isCurrentPlayer) {
                                                $step = 13;
                                                continue;
                                            } else  {
                                                $step = 23;
                                                continue;
                                            }
                                    }
                                    case 13: {
                                        best = null;
                                            i = 0;
                                            $step = 14;
                                            continue;
                                    }
                                    case 14: {
                                        if ( i < result.Moves.Count ) {
                                                $step = 15;
                                                continue;
                                            }
                                        $step = 21;
                                        continue;
                                    }
                                    case 15: {
                                        move = result.Moves.getItem(i);

                                            invoke = this.AlphaBetaHelper(ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, move), ((depth - 1) | 0), alpha, beta, false);

                                            temp = null;
                                            $t = Bridge.getEnumerator(invoke);
                                            $step = 16;
                                            continue;
                                    }
                                    case 16: {
                                        if ($t.moveNext()) {
                                                x = $t.Current;
                                                $step = 17;
                                                continue;
                                            }
                                        $step = 19;
                                        continue;
                                    }
                                    case 17: {
                                        $enumerator.current = null;
                                            $step = 18;
                                            return true;
                                    }
                                    case 18: {
                                        if (x != null) {
                                                temp = Bridge.cast(Bridge.unbox(x, System.Int32), System.Int32, true);
                                            }
                                        $step = 16;
                                        continue;
                                    }
                                    case 19: {
                                        if (temp == null) {
                                                throw new System.Exception();
                                            }
                                            moveValue = System.Nullable.getValue(temp);

                                            if (best == null || moveValue >= System.Nullable.getValue(best)) {
                                                best = moveValue;
                                            }

                                            if (alpha == null || System.Nullable.getValue(best) >= System.Nullable.getValue(alpha)) {
                                                alpha = System.Nullable.getValue(best);
                                            }

                                            if (System.Nullable.hasValue(alpha) && System.Nullable.hasValue(beta) && System.Nullable.getValue(alpha) >= System.Nullable.getValue(beta)) {
                                                $step = 21;
                                                continue;
                                            }
                                        $step = 20;
                                        continue;
                                    }
                                    case 20: {
                                        i = (i + 1) | 0;
                                        $step = 14;
                                        continue;
                                    }
                                    case 21: {
                                        if (best == null) {
                                                throw new System.Exception();
                                            }

                                            $enumerator.current = Bridge.box(best, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 22;
                                            return true;
                                    }
                                    case 22: {
                                        return false;
                                    }
                                    case 23: {
                                        best1 = null;
                                            i1 = 0;
                                            $step = 24;
                                            continue;
                                    }
                                    case 24: {
                                        if ( i1 < result.Moves.Count ) {
                                                $step = 25;
                                                continue;
                                            }
                                        $step = 31;
                                        continue;
                                    }
                                    case 25: {
                                        move1 = result.Moves.getItem(i1);

                                            invoke1 = this.AlphaBetaHelper(ChessCompStompWithHacksEngine.MoveImplementation.ApplyMove$1(gameState, move1), ((depth - 1) | 0), alpha, beta, true);

                                            temp1 = null;
                                            $t1 = Bridge.getEnumerator(invoke1);
                                            $step = 26;
                                            continue;
                                    }
                                    case 26: {
                                        if ($t1.moveNext()) {
                                                x1 = $t1.Current;
                                                $step = 27;
                                                continue;
                                            }
                                        $step = 29;
                                        continue;
                                    }
                                    case 27: {
                                        $enumerator.current = null;
                                            $step = 28;
                                            return true;
                                    }
                                    case 28: {
                                        if (x1 != null) {
                                                temp1 = Bridge.cast(Bridge.unbox(x1, System.Int32), System.Int32, true);
                                            }
                                        $step = 26;
                                        continue;
                                    }
                                    case 29: {
                                        if (temp1 == null) {
                                                throw new System.Exception();
                                            }
                                            moveValue1 = System.Nullable.getValue(temp1);

                                            if (best1 == null || moveValue1 <= System.Nullable.getValue(best1)) {
                                                best1 = moveValue1;
                                            }

                                            if (beta == null || System.Nullable.getValue(best1) <= System.Nullable.getValue(beta)) {
                                                beta = System.Nullable.getValue(best1);
                                            }

                                            if (System.Nullable.hasValue(alpha) && System.Nullable.hasValue(beta) && System.Nullable.getValue(beta) <= System.Nullable.getValue(alpha)) {
                                                $step = 31;
                                                continue;
                                            }
                                        $step = 30;
                                        continue;
                                    }
                                    case 30: {
                                        i1 = (i1 + 1) | 0;
                                        $step = 24;
                                        continue;
                                    }
                                    case 31: {
                                        if (best1 == null) {
                                                throw new System.Exception();
                                            }
                                            $enumerator.current = Bridge.box(best1, System.Int32, System.Nullable.toString, System.Nullable.getHashCode);
                                            $step = 32;
                                            return true;
                                    }
                                    case 32: {
                                        return false;
                                    }
                                    case 33: {

                                    }
                                    default: {
                                        return false;
                                    }
                                }
                            }
                        } catch($async_e1) {
                            $async_e = System.Exception.create($async_e1);
                            throw $async_e;
                        }
                    }));
                    return $enumerator;
                }, arguments));
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.RandomizedBoardEvaluator", {
        inherits: [ChessCompStompWithHacksEngine.IBoardEvaluator],
        fields: {
            random: null,
            pawnValue: 0,
            knightValue: 0,
            bishopValue: 0,
            rookValue: 0,
            queenValue: 0,
            kingValue: 0
        },
        alias: ["Evaluate", "ChessCompStompWithHacksEngine$IBoardEvaluator$Evaluate"],
        ctors: {
            ctor: function (random) {
                this.$initialize();
                this.random = random;
                this.pawnValue = (random.DTLibrary$IDTRandom$NextInt(61) + 70) | 0;
                this.knightValue = (random.DTLibrary$IDTRandom$NextInt(201) + 200) | 0;
                this.bishopValue = (random.DTLibrary$IDTRandom$NextInt(201) + 200) | 0;
                this.rookValue = (random.DTLibrary$IDTRandom$NextInt(201) + 400) | 0;
                this.queenValue = (random.DTLibrary$IDTRandom$NextInt(201) + 800) | 0;
                this.kingValue = (random.DTLibrary$IDTRandom$NextInt(201) + 300) | 0;
            }
        },
        methods: {
            Evaluate: function (gameState, isWhite) {
                var whiteScore = 0;
                var blackScore = 0;

                for (var i = 0; i < 8; i = (i + 1) | 0) {
                    for (var j = 0; j < 8; j = (j + 1) | 0) {
                        switch (gameState.Board.GetPiece$1(i, j)) {
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn: 
                                whiteScore = (whiteScore + this.pawnValue) | 0;
                                whiteScore = (whiteScore + (Bridge.Int.mul(10, (((j - 1) | 0))))) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight: 
                                whiteScore = (whiteScore + this.knightValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop: 
                                whiteScore = (whiteScore + this.bishopValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook: 
                                whiteScore = (whiteScore + this.rookValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen: 
                                whiteScore = (whiteScore + this.queenValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing: 
                                whiteScore = (whiteScore + this.kingValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn: 
                                blackScore = (blackScore + this.pawnValue) | 0;
                                blackScore = (blackScore + (Bridge.Int.mul(10, (((6 - j) | 0))))) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight: 
                                blackScore = (blackScore + this.knightValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop: 
                                blackScore = (blackScore + this.bishopValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook: 
                                blackScore = (blackScore + this.rookValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen: 
                                blackScore = (blackScore + this.queenValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing: 
                                blackScore = (blackScore + this.kingValue) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.Empty: 
                                break;
                            default: 
                                throw new System.Exception();
                        }
                    }
                }

                // Note that neither score can be zero since the king also gives points
                var score;
                if (whiteScore > blackScore) {
                    score = (Bridge.Int.div(Bridge.Int.mul(whiteScore, 10000), blackScore)) | 0;
                } else {
                    if (blackScore > whiteScore) {
                        score = (Bridge.Int.div(Bridge.Int.mul(((-blackScore) | 0), 10000), whiteScore)) | 0;
                    } else {
                        score = 0;
                    }
                }

                if (isWhite) {
                    return score;
                } else {
                    return ((-score) | 0);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.RandomMoveAI", {
        inherits: [ChessCompStompWithHacksEngine.IChessAI],
        fields: {
            startTimeMicroSeconds: System.Int64(0),
            bestMove: null
        },
        alias: [
            "GetStartTimeMicroSeconds", "ChessCompStompWithHacksEngine$IChessAI$GetStartTimeMicroSeconds",
            "GetBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetBestMoveFoundSoFar",
            "HasFinishedCalculation", "ChessCompStompWithHacksEngine$IChessAI$HasFinishedCalculation",
            "GetDepthOfBestMoveFoundSoFar", "ChessCompStompWithHacksEngine$IChessAI$GetDepthOfBestMoveFoundSoFar",
            "CalculateBestMove", "ChessCompStompWithHacksEngine$IChessAI$CalculateBestMove"
        ],
        ctors: {
            ctor: function (gameState, timer, random) {
                this.$initialize();
                this.startTimeMicroSeconds = timer.DTLibrary$ITimer$GetNumberOfMicroSeconds();

                var result = ChessCompStompWithHacksEngine.ComputeMoves.GetMoves(gameState);

                if (result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.InProgress) {
                    this.bestMove = result.Moves.getItem(random.DTLibrary$IDTRandom$NextInt(result.Moves.Count));
                } else {
                    this.bestMove = null;
                }
            }
        },
        methods: {
            GetStartTimeMicroSeconds: function () {
                return this.startTimeMicroSeconds;
            },
            GetBestMoveFoundSoFar: function () {
                if (this.bestMove == null) {
                    throw new System.Exception();
                }

                return this.bestMove;
            },
            HasFinishedCalculation: function () {
                return true;
            },
            GetDepthOfBestMoveFoundSoFar: function () {
                return 0;
            },
            CalculateBestMove: function (millisecondsToThink) {
                // do nothing
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksEngine.StandardBoardEvaluator", {
        inherits: [ChessCompStompWithHacksEngine.IBoardEvaluator],
        fields: {
            random: null
        },
        alias: ["Evaluate", "ChessCompStompWithHacksEngine$IBoardEvaluator$Evaluate"],
        ctors: {
            ctor: function (random) {
                this.$initialize();
                this.random = random;
            }
        },
        methods: {
            Evaluate: function (gameState, isWhite) {
                var whiteScore = 0;
                var blackScore = 0;

                for (var i = 0; i < 8; i = (i + 1) | 0) {
                    for (var j = 0; j < 8; j = (j + 1) | 0) {
                        switch (gameState.Board.GetPiece$1(i, j)) {
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhitePawn: 
                                whiteScore = (whiteScore + 100) | 0;
                                whiteScore = (whiteScore + (Bridge.Int.mul(10, (((j - 1) | 0))))) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKnight: 
                                whiteScore = (whiteScore + 300) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteBishop: 
                                whiteScore = (whiteScore + 300) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteRook: 
                                whiteScore = (whiteScore + 500) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteQueen: 
                                whiteScore = (whiteScore + 900) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.WhiteKing: 
                                whiteScore = (whiteScore + 400) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackPawn: 
                                blackScore = (blackScore + 100) | 0;
                                blackScore = (blackScore + (Bridge.Int.mul(10, (((6 - j) | 0))))) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKnight: 
                                blackScore = (blackScore + 300) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackBishop: 
                                blackScore = (blackScore + 300) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackRook: 
                                blackScore = (blackScore + 500) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackQueen: 
                                blackScore = (blackScore + 900) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.BlackKing: 
                                blackScore = (blackScore + 400) | 0;
                                break;
                            case ChessCompStompWithHacksEngine.ChessSquarePiece.Empty: 
                                break;
                            default: 
                                throw new System.Exception();
                        }
                    }
                }

                // Note that neither score can be zero since the king gives 400 points
                var score;
                if (whiteScore > blackScore) {
                    score = (Bridge.Int.div(Bridge.Int.mul(whiteScore, 10000), blackScore)) | 0;
                } else {
                    if (blackScore > whiteScore) {
                        score = (Bridge.Int.div(Bridge.Int.mul(((-blackScore) | 0), 10000), whiteScore)) | 0;
                    } else {
                        score = 0;
                    }
                }

                if (isWhite) {
                    return score;
                } else {
                    return ((-score) | 0);
                }
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GameLogic.ChessPiecesRendererMouse", {
        inherits: [DTLibrary.IMouse],
        $kind: "nested class",
        fields: {
            mouse: null
        },
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function (mouse) {
                this.$initialize();
                this.mouse = new DTLibrary.TranslatedMouse(mouse, -186, -50);
            }
        },
        methods: {
            GetX: function () {
                return this.mouse.GetX();
            },
            GetY: function () {
                return this.mouse.GetY();
            },
            IsLeftMouseButtonPressed: function () {
                return this.mouse.IsLeftMouseButtonPressed();
            },
            IsRightMouseButtonPressed: function () {
                return this.mouse.IsRightMouseButtonPressed();
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GameLogic.MoveTrackerRendererMouse", {
        inherits: [DTLibrary.IMouse],
        $kind: "nested class",
        fields: {
            mouse: null
        },
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function (mouse) {
                this.$initialize();
                this.mouse = new DTLibrary.TranslatedMouse(mouse, -720, -208);
            }
        },
        methods: {
            GetX: function () {
                return this.mouse.GetX();
            },
            GetY: function () {
                return this.mouse.GetY();
            },
            IsLeftMouseButtonPressed: function () {
                return this.mouse.IsLeftMouseButtonPressed();
            },
            IsRightMouseButtonPressed: function () {
                return this.mouse.IsRightMouseButtonPressed();
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.GameLogic.NukeRendererMouse", {
        inherits: [DTLibrary.IMouse],
        $kind: "nested class",
        fields: {
            mouse: null
        },
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function (mouse) {
                this.$initialize();
                this.mouse = new DTLibrary.TranslatedMouse(mouse, -25, -50);
            }
        },
        methods: {
            GetX: function () {
                return this.mouse.GetX();
            },
            GetY: function () {
                return this.mouse.GetY();
            },
            IsLeftMouseButtonPressed: function () {
                return this.mouse.IsLeftMouseButtonPressed();
            },
            IsRightMouseButtonPressed: function () {
                return this.mouse.IsRightMouseButtonPressed();
            }
        }
    });

    Bridge.define("DTLibrary.ConsoleLogger", {
        inherits: [DTLibrary.IDTLogger],
        alias: [
            "Write", "DTLibrary$IDTLogger$Write",
            "WriteLine$1", "DTLibrary$IDTLogger$WriteLine$1",
            "WriteLine", "DTLibrary$IDTLogger$WriteLine"
        ],
        methods: {
            Write: function (str) {
                System.Console.Write(str);
            },
            WriteLine$1: function (str) {
                System.Console.WriteLine(str);
            },
            WriteLine: function () {
                System.Console.WriteLine();
            }
        }
    });

    /**
     * CopiedKeyboard is just an easy way to make a deep copy
     of an IKeyboard object.  Its constructor takes an IKeyboard
     object in order to create a copy of the keyboard.
     In general, making a copy of the IKeyboard object can
     be useful, since this copy is immutable and is guaranteed
     not to change.
     *
     * @public
     * @class DTLibrary.CopiedKeyboard
     * @implements  DTLibrary.IKeyboard
     */
    Bridge.define("DTLibrary.CopiedKeyboard", {
        inherits: [DTLibrary.IKeyboard],
        fields: {
            mapping: null
        },
        alias: ["IsPressed", "DTLibrary$IKeyboard$IsPressed"],
        ctors: {
            ctor: function (keyboard) {
                var $t;
                this.$initialize();
                this.mapping = new (System.Collections.Generic.Dictionary$2(DTLibrary.Key,System.Boolean))();
                $t = Bridge.getEnumerator(System.Enum.getValues(DTLibrary.Key));
                try {
                    while ($t.moveNext()) {
                        var key = Bridge.cast($t.Current, DTLibrary.Key);
                        this.mapping.set(key, keyboard.DTLibrary$IKeyboard$IsPressed(key));
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }
            }
        },
        methods: {
            IsPressed: function (key) {
                return this.mapping.get(key);
            }
        }
    });

    /**
     * CopiedMouse is just an easy way to make a deep copy
     of an IMouse object.  Its constructor takes an IMouse
     object in order to create a copy of the mouse.
     In general, making a copy of the IMouse object can
     be useful, since this copy is immutable and is guaranteed
     not to change.
     *
     * @public
     * @class DTLibrary.CopiedMouse
     * @implements  DTLibrary.IMouse
     */
    Bridge.define("DTLibrary.CopiedMouse", {
        inherits: [DTLibrary.IMouse],
        fields: {
            x: 0,
            y: 0,
            leftMouse: false,
            rightMouse: false
        },
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function (mouse) {
                this.$initialize();
                this.x = mouse.DTLibrary$IMouse$GetX();
                this.y = mouse.DTLibrary$IMouse$GetY();
                this.leftMouse = mouse.DTLibrary$IMouse$IsLeftMouseButtonPressed();
                this.rightMouse = mouse.DTLibrary$IMouse$IsRightMouseButtonPressed();
            }
        },
        methods: {
            GetX: function () {
                return this.x;
            },
            GetY: function () {
                return this.y;
            },
            IsLeftMouseButtonPressed: function () {
                return this.leftMouse;
            },
            IsRightMouseButtonPressed: function () {
                return this.rightMouse;
            }
        }
    });

    /**
     * An implementation of IDTRandom that simply
     uses the System.Random class.
     *
     * @public
     * @class DTLibrary.DTRandom
     * @implements  DTLibrary.IDTRandom
     */
    Bridge.define("DTLibrary.DTRandom", {
        inherits: [DTLibrary.IDTRandom],
        fields: {
            random: null
        },
        alias: [
            "AddSeed", "DTLibrary$IDTRandom$AddSeed",
            "NextInt", "DTLibrary$IDTRandom$NextInt",
            "NextBool", "DTLibrary$IDTRandom$NextBool"
        ],
        ctors: {
            ctor: function () {
                this.$initialize();
                this.random = new System.Random.ctor();
            }
        },
        methods: {
            AddSeed: function (i) {
                this.random = new System.Random.$ctor1(i);
            },
            NextInt: function (i) {
                return this.random.Next$1(i);
            },
            NextBool: function () {
                return this.NextInt(2) === 1;
            }
        }
    });

    /**
     * An implementation of IKeyboard that simply represents
     no input (i.e. no keys are pressed).
     *
     * @public
     * @class DTLibrary.EmptyKeyboard
     * @implements  DTLibrary.IKeyboard
     */
    Bridge.define("DTLibrary.EmptyKeyboard", {
        inherits: [DTLibrary.IKeyboard],
        alias: ["IsPressed", "DTLibrary$IKeyboard$IsPressed"],
        ctors: {
            ctor: function () {
                this.$initialize();
            }
        },
        methods: {
            IsPressed: function (key) {
                return false;
            }
        }
    });

    Bridge.define("DTLibrary.EmptyLogger", {
        inherits: [DTLibrary.IDTLogger],
        alias: [
            "Write", "DTLibrary$IDTLogger$Write",
            "WriteLine$1", "DTLibrary$IDTLogger$WriteLine$1",
            "WriteLine", "DTLibrary$IDTLogger$WriteLine"
        ],
        methods: {
            Write: function (str) { },
            WriteLine$1: function (str) { },
            WriteLine: function () { }
        }
    });

    /**
     * An implementation of IMouse that simply represents
     no input.
     *
     * @public
     * @class DTLibrary.EmptyMouse
     * @implements  DTLibrary.IMouse
     */
    Bridge.define("DTLibrary.EmptyMouse", {
        inherits: [DTLibrary.IMouse],
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function () {
                this.$initialize();
            }
        },
        methods: {
            GetX: function () {
                return 0;
            },
            GetY: function () {
                return 0;
            },
            IsLeftMouseButtonPressed: function () {
                return false;
            },
            IsRightMouseButtonPressed: function () {
                return false;
            }
        }
    });

    /**
     * An interface that marks an implementation of IDTRandom as completely deterministic.
     Deterministic is defined to mean that the implementation will always
     return the same values given the same seed and sequence of function calls.
     This means that an instance of IDTDeterministicRandom must behave identically
     across a variety of dimensions.
     For instance:
     * Two instances on different computers (with the same seed and function calls)
       must return the same values.
     * Two instances being executed at different times (with the same seed and function calls)
       must return the same values.
     * Two instances being executed on different versions of C# (with the same seed and function calls)
       must return the same values.
     * Two instances being executed on different operating systems (with the same seed and function calls)
       must return the same values.
     *
     * @abstract
     * @public
     * @class DTLibrary.IDTDeterministicRandom
     * @implements  DTLibrary.IDTRandom
     */
    Bridge.define("DTLibrary.IDTDeterministicRandom", {
        inherits: [DTLibrary.IDTRandom],
        $kind: "interface"
    });

    Bridge.define("DTLibrary.SimpleTimer", {
        inherits: [DTLibrary.ITimer],
        alias: ["GetNumberOfMicroSeconds", "DTLibrary$ITimer$GetNumberOfMicroSeconds"],
        methods: {
            GetNumberOfMicroSeconds: function () {
                return System.DateTime.getTicks(System.DateTime.getNow()).div(System.Int64(10));
            }
        }
    });

    Bridge.define("DTLibrary.SimulatedMouse", {
        inherits: [DTLibrary.IMouse],
        fields: {
            x: 0,
            y: 0,
            isLeftMouseButtonPressed: false,
            isRightMouseButtonPressed: false
        },
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function (x, y, isLeftMouseButtonPressed, isRightMouseButtonPressed) {
                this.$initialize();
                this.x = x;
                this.y = y;
                this.isLeftMouseButtonPressed = isLeftMouseButtonPressed;
                this.isRightMouseButtonPressed = isRightMouseButtonPressed;
            }
        },
        methods: {
            GetX: function () {
                return this.x;
            },
            GetY: function () {
                return this.y;
            },
            IsLeftMouseButtonPressed: function () {
                return this.isLeftMouseButtonPressed;
            },
            IsRightMouseButtonPressed: function () {
                return this.isRightMouseButtonPressed;
            }
        }
    });

    Bridge.define("DTLibrary.TranslatedDisplayOutput$2", function (ImageEnum, FontEnum) { return {
        inherits: [DTLibrary.IDisplayOutput$2(ImageEnum,FontEnum)],
        fields: {
            display: null,
            xOffsetInPixels: 0,
            yOffsetInPixels: 0
        },
        alias: [
            "DrawRectangle", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle",
            "DrawText", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawText",
            "DrawInitialLoadingScreen", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawInitialLoadingScreen",
            "DrawImage", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawImage",
            "DrawImageRotatedClockwise$1", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawImageRotatedClockwise",
            "DrawImageRotatedClockwise", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawImageRotatedClockwise$1",
            "GetWidth", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$GetWidth",
            "GetHeight", "DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$GetHeight"
        ],
        ctors: {
            ctor: function (display, xOffsetInPixels, yOffsetInPixels) {
                this.$initialize();
                this.display = display;
                this.xOffsetInPixels = xOffsetInPixels;
                this.yOffsetInPixels = yOffsetInPixels;
            }
        },
        methods: {
            DrawRectangle: function (x, y, width, height, color, fill) {
                this.display["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawRectangle"](((x + this.xOffsetInPixels) | 0), ((y + this.yOffsetInPixels) | 0), width, height, color, fill);
            },
            DrawText: function (x, y, text, font, color) {
                this.display["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawText"](((x + this.xOffsetInPixels) | 0), ((y + this.yOffsetInPixels) | 0), text, font, color);
            },
            DrawInitialLoadingScreen: function () {
                this.display["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawInitialLoadingScreen"]();
            },
            DrawImage: function (image, x, y) {
                this.DrawImageRotatedClockwise(image, x, y, 0, 128);
            },
            DrawImageRotatedClockwise$1: function (image, x, y, degreesScaled) {
                this.DrawImageRotatedClockwise(image, x, y, degreesScaled, 128);
            },
            DrawImageRotatedClockwise: function (image, x, y, degreesScaled, scalingFactorScaled) {
                this.display["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$DrawImageRotatedClockwise$1"](image, ((x + this.xOffsetInPixels) | 0), ((y + this.yOffsetInPixels) | 0), degreesScaled, scalingFactorScaled);
            },
            GetWidth: function (image) {
                return this.display["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$GetWidth"](image);
            },
            GetHeight: function (image) {
                return this.display["DTLibrary$IDisplayOutput$2$" + Bridge.getTypeAlias(ImageEnum) + "$" + Bridge.getTypeAlias(FontEnum) + "$GetHeight"](image);
            }
        }
    }; });

    /**
     * An implementation of IMouse that takes an existing IMouse object (in the constructor)
     and creates an IMouse implementation that's simply the same mouse input, but translated
     by some offset.
     *
     * @public
     * @class DTLibrary.TranslatedMouse
     * @implements  DTLibrary.IMouse
     */
    Bridge.define("DTLibrary.TranslatedMouse", {
        inherits: [DTLibrary.IMouse],
        fields: {
            x: 0,
            y: 0,
            pressedLeft: false,
            pressedRight: false
        },
        alias: [
            "GetX", "DTLibrary$IMouse$GetX",
            "GetY", "DTLibrary$IMouse$GetY",
            "IsLeftMouseButtonPressed", "DTLibrary$IMouse$IsLeftMouseButtonPressed",
            "IsRightMouseButtonPressed", "DTLibrary$IMouse$IsRightMouseButtonPressed"
        ],
        ctors: {
            ctor: function (mouse, xOffset, yOffset) {
                this.$initialize();
                this.x = (mouse.DTLibrary$IMouse$GetX() + xOffset) | 0;
                this.y = (mouse.DTLibrary$IMouse$GetY() + yOffset) | 0;
                this.pressedLeft = mouse.DTLibrary$IMouse$IsLeftMouseButtonPressed();
                this.pressedRight = mouse.DTLibrary$IMouse$IsRightMouseButtonPressed();
            }
        },
        methods: {
            GetX: function () {
                return this.x;
            },
            GetY: function () {
                return this.y;
            },
            IsLeftMouseButtonPressed: function () {
                return this.pressedLeft;
            },
            IsRightMouseButtonPressed: function () {
                return this.pressedRight;
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessFont", {
        $kind: "enum",
        statics: {
            fields: {
                Fetamont12Pt: 0,
                Fetamont14Pt: 1,
                Fetamont16Pt: 2,
                Fetamont18Pt: 3,
                Fetamont20Pt: 4,
                Fetamont32Pt: 5
            }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeMusic", {
        inherits: [DTLibrary.IMusic$1(ChessCompStompWithHacksLibrary.ChessMusic)],
        alias: [
            "LoadMusic", "DTLibrary$IMusicProcessing$LoadMusic",
            "PlayMusic", "DTLibrary$IMusicOutput$1$ChessCompStompWithHacksLibrary$ChessMusic$PlayMusic",
            "StopMusic", "DTLibrary$IMusicOutput$1$ChessCompStompWithHacksLibrary$ChessMusic$StopMusic",
            "DisposeMusic", "DTLibrary$IMusicCleanup$DisposeMusic"
        ],
        ctors: {
            ctor: function () {
                this.$initialize();
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeMusicJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\t\r\n\t\t\t\t\tvar musicDictionary = {};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar numberOfAudioObjectsLoaded = 0;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar loadMusic = function (musicNames) {\r\n\t\t\t\t\t\tvar musicNamesArray = musicNames.split(',');\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar numberOfAudioObjects = musicNamesArray.length;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < musicNamesArray.length; i++) {\r\n\t\t\t\t\t\t\tvar musicName = musicNamesArray[i];\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (musicDictionary[musicName])\r\n\t\t\t\t\t\t\t\tcontinue;\r\n\t\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tvar musicPath = 'Data/Music/' + musicName + '?doNotCache=' + Date.now().toString();\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tvar audio = new Audio(musicPath);\r\n\t\t\t\t\t\t\taudio.addEventListener('canplaythrough', function () {\r\n\t\t\t\t\t\t\t\tnumberOfAudioObjectsLoaded++;\r\n\t\t\t\t\t\t\t});\r\n\t\t\t\t\t\t\taudio.loop = true;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tmusicDictionary[musicName] = audio;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\treturn numberOfAudioObjects === numberOfAudioObjectsLoaded;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar playMusic = function (musicName, volume) {\r\n\t\t\t\t\t\tvar music = musicDictionary[musicName];\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (volume > 1.0)\r\n\t\t\t\t\t\t\tvolume = 1.0;\r\n\t\t\t\t\t\tif (volume < 0.0)\r\n\t\t\t\t\t\t\tvolume = 0.0;\r\n\t\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var m in musicDictionary) {\r\n\t\t\t\t\t\t\tvar audio = musicDictionary[m];\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (audio === music) {\r\n\t\t\t\t\t\t\t\taudio.volume = volume;\r\n\t\t\t\t\t\t\t\tvar audioPromise = audio.play();\r\n\t\t\t\t\t\t\t\tif (audioPromise) {\r\n\t\t\t\t\t\t\t\t\taudioPromise.then(function () {}, function () {});\r\n\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t} else {\r\n\t\t\t\t\t\t\t\taudio.pause();\r\n\t\t\t\t\t\t\t\taudio.currentTime = 0;\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar stopMusic = function () {\r\n\t\t\t\t\t\tfor (var musicName in musicDictionary) {\r\n\t\t\t\t\t\t\tvar audio = musicDictionary[musicName];\r\n\t\t\t\t\t\t\taudio.pause();\r\n\t\t\t\t\t\t\taudio.currentTime = 0;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tloadMusic: loadMusic,\r\n\t\t\t\t\t\tplayMusic: playMusic,\r\n\t\t\t\t\t\tstopMusic: stopMusic\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            LoadMusic: function () {
                var $t;
                var musicNames = "";
                var isFirst = true;
                $t = Bridge.getEnumerator(System.Enum.getValues(ChessCompStompWithHacksLibrary.ChessMusic));
                try {
                    while ($t.moveNext()) {
                        var chessMusic = Bridge.cast($t.Current, ChessCompStompWithHacksLibrary.ChessMusic);
                        if (isFirst) {
                            isFirst = false;
                        } else {
                            musicNames = (musicNames || "") + ",";
                        }
                        musicNames = (musicNames || "") + (ChessCompStompWithHacksLibrary.ChessMusicUtil.GetMusicFilename(chessMusic) || "");
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                if (Bridge.referenceEquals(musicNames, "")) {
                    return true;
                }

                var result = eval("window.ChessCompStompWithHacksBridgeMusicJavascript.loadMusic('" + (musicNames || "") + "')");

                return result;
            },
            PlayMusic: function (music, volume) {
                var finalVolume = (ChessCompStompWithHacksLibrary.ChessMusicUtil.GetMusicVolume(music) / 100.0) * (volume / 100.0);
                if (finalVolume > 1.0) {
                    finalVolume = 1.0;
                }
                if (finalVolume < 0.0) {
                    finalVolume = 0.0;
                }

                window.ChessCompStompWithHacksBridgeMusicJavascript.playMusic(ChessCompStompWithHacksLibrary.ChessMusicUtil.GetMusicFilename(music), finalVolume);
            },
            StopMusic: function () {
                window.ChessCompStompWithHacksBridgeMusicJavascript.stopMusic();
            },
            DisposeMusic: function () { }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeSoundOutput", {
        inherits: [DTLibrary.ISoundOutput$1(ChessCompStompWithHacksLibrary.ChessSound)],
        fields: {
            desiredSoundVolume: 0,
            currentSoundVolume: 0,
            elapsedMicrosPerFrame: 0
        },
        alias: [
            "LoadSounds", "DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$LoadSounds",
            "SetSoundVolume", "DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$SetSoundVolume",
            "GetSoundVolume", "DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$GetSoundVolume",
            "ProcessFrame", "DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$ProcessFrame",
            "PlaySound", "DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$PlaySound",
            "DisposeSounds", "DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$DisposeSounds"
        ],
        ctors: {
            ctor: function (elapsedMicrosPerFrame) {
                this.$initialize();
                this.desiredSoundVolume = ChessCompStompWithHacksLibrary.GlobalState.DEFAULT_VOLUME;
                this.currentSoundVolume = this.desiredSoundVolume;
                this.elapsedMicrosPerFrame = elapsedMicrosPerFrame;
                eval("\r\n\t\t\t\twindow.ChessCompStompWithHacksBridgeSoundOutputJavascript = ((function () {\r\n\t\t\t\t\t'use strict';\r\n\t\t\t\t\t\t\r\n\t\t\t\t\tvar soundDictionary = {};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar numberOfAudioObjectsLoaded = 0;\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar loadSounds = function (soundNames) {\r\n\t\t\t\t\t\tvar soundNamesArray = soundNames.split(',');\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar numberOfAudioObjects = soundNamesArray.length * 10;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < soundNamesArray.length; i++) {\r\n\t\t\t\t\t\t\tvar soundName = soundNamesArray[i];\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tif (soundDictionary[soundName])\r\n\t\t\t\t\t\t\t\tcontinue;\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tsoundDictionary[soundName] = [];\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tvar soundPath = 'Data/Sound/' + soundName + '?doNotCache=' + Date.now().toString();\r\n\t\t\t\t\t\t\tfor (var j = 0; j < 10; j++) {\r\n\t\t\t\t\t\t\t\tvar audio = new Audio(soundPath);\r\n\t\t\t\t\t\t\t\taudio.addEventListener('canplaythrough', function () {\r\n\t\t\t\t\t\t\t\t\tnumberOfAudioObjectsLoaded++;\r\n\t\t\t\t\t\t\t\t});\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tsoundDictionary[soundName].push(audio);\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\treturn numberOfAudioObjects === numberOfAudioObjectsLoaded;\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\tvar playSound = function (soundName, volume) {\r\n\t\t\t\t\t\tvar sound = soundDictionary[soundName];\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tif (volume > 1.0)\r\n\t\t\t\t\t\t\tvolume = 1.0;\r\n\t\t\t\t\t\tif (volume < 0.0)\r\n\t\t\t\t\t\t\tvolume = 0.0;\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tvar audio = sound[0];\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tfor (var i = 0; i < sound.length; i++) {\r\n\t\t\t\t\t\t\tif (i === sound.length - 1)\r\n\t\t\t\t\t\t\t\tsound[i] = audio;\r\n\t\t\t\t\t\t\telse\r\n\t\t\t\t\t\t\t\tsound[i] = sound[i+1];\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\taudio.volume = volume;\r\n\t\t\t\t\t\taudio.play();\r\n\t\t\t\t\t};\r\n\t\t\t\t\t\r\n\t\t\t\t\treturn {\r\n\t\t\t\t\t\tloadSounds: loadSounds,\r\n\t\t\t\t\t\tplaySound: playSound\r\n\t\t\t\t\t};\r\n\t\t\t\t})());\r\n\t\t\t");
            }
        },
        methods: {
            LoadSounds: function () {
                var $t;
                var soundNames = "";
                var isFirst = true;
                $t = Bridge.getEnumerator(System.Enum.getValues(ChessCompStompWithHacksLibrary.ChessSound));
                try {
                    while ($t.moveNext()) {
                        var chessSound = Bridge.cast($t.Current, ChessCompStompWithHacksLibrary.ChessSound);
                        if (isFirst) {
                            isFirst = false;
                        } else {
                            soundNames = (soundNames || "") + ",";
                        }
                        soundNames = (soundNames || "") + (ChessCompStompWithHacksLibrary.ChessSoundUtil.GetSoundFilename(chessSound) || "");
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                if (Bridge.referenceEquals(soundNames, "")) {
                    return true;
                }

                var result = eval("window.ChessCompStompWithHacksBridgeSoundOutputJavascript.loadSounds('" + (soundNames || "") + "')");

                return result;
            },
            /**
             * Volume ranges from 0 to 100 (both inclusive)
             *
             * @instance
             * @public
             * @this ChessCompStompWithHacks.BridgeSoundOutput
             * @memberof ChessCompStompWithHacks.BridgeSoundOutput
             * @param   {number}    volume
             * @return  {void}
             */
            SetSoundVolume: function (volume) {
                if (volume < 0) {
                    throw new System.Exception();
                }

                if (volume > 100) {
                    throw new System.Exception();
                }

                this.desiredSoundVolume = volume;
            },
            GetSoundVolume: function () {
                return this.desiredSoundVolume;
            },
            ProcessFrame: function () {
                this.currentSoundVolume = DTLibrary.VolumeUtil.GetVolumeSmoothed(this.elapsedMicrosPerFrame, this.currentSoundVolume, this.desiredSoundVolume);
            },
            PlaySound: function (sound) {
                var finalVolume = (ChessCompStompWithHacksLibrary.ChessSoundUtil.GetSoundVolume(sound) / 100.0) * (this.currentSoundVolume / 100.0);
                if (finalVolume > 1.0) {
                    finalVolume = 1.0;
                }
                if (finalVolume < 0.0) {
                    finalVolume = 0.0;
                }

                if (finalVolume > 0.0) {
                    window.ChessCompStompWithHacksBridgeSoundOutputJavascript.playSound(ChessCompStompWithHacksLibrary.ChessSoundUtil.GetSoundFilename(sound), finalVolume);
                }
            },
            DisposeSounds: function () { }
        }
    });

    Bridge.define("ChessCompStompWithHacks.BridgeDisplay", {
        inherits: [DTLibrary.DTDisplay$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont)],
        fields: {
            bridgeDisplayRectangle: null,
            bridgeDisplayImages: null,
            bridgeDisplayFont: null
        },
        alias: [
            "DrawInitialLoadingScreen", "DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawInitialLoadingScreen",
            "DrawRectangle", "DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle",
            "LoadImages", "DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$LoadImages",
            "DrawImageRotatedClockwise", "DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawImageRotatedClockwise$1",
            "GetWidth", "DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetWidth",
            "GetWidth", "DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetWidth",
            "GetHeight", "DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$GetHeight",
            "GetHeight", "DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$GetHeight",
            "DrawText", "DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText",
            "DisposeImages", "DTLibrary$IDisplayCleanup$DisposeImages"
        ],
        ctors: {
            ctor: function () {
                this.$initialize();
                DTLibrary.DTDisplay$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont).ctor.call(this);
                this.bridgeDisplayRectangle = new ChessCompStompWithHacks.BridgeDisplayRectangle();
                this.bridgeDisplayImages = new ChessCompStompWithHacks.BridgeDisplayImages();
                this.bridgeDisplayFont = new ChessCompStompWithHacks.BridgeDisplayFont();
            }
        },
        methods: {
            DrawInitialLoadingScreen: function () { },
            DrawRectangle: function (x, y, width, height, color, fill) {
                this.bridgeDisplayRectangle.DrawRectangle(x, y, width, height, color, fill);
            },
            LoadImages: function () {
                var finishedLoadingImages = this.bridgeDisplayImages.LoadImages();

                if (!finishedLoadingImages) {
                    return false;
                }

                return this.bridgeDisplayFont.LoadFonts();
            },
            DrawImageRotatedClockwise: function (image, x, y, degreesScaled, scalingFactorScaled) {
                this.bridgeDisplayImages.DrawImageRotatedClockwise(image, x, y, degreesScaled, scalingFactorScaled);
            },
            GetWidth: function (image) {
                return this.bridgeDisplayImages.GetWidth(image);
            },
            GetHeight: function (image) {
                return this.bridgeDisplayImages.GetHeight(image);
            },
            DrawText: function (x, y, text, font, color) {
                this.bridgeDisplayFont.DrawText(x, y, text, font, color);
            },
            DisposeImages: function () { }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.AIMessageFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        statics: {
            fields: {
                PANEL_WIDTH: 0,
                PANEL_HEIGHT: 0,
                PANEL_X: 0,
                PANEL_Y: 0
            },
            ctors: {
                init: function () {
                    this.PANEL_WIDTH = 480;
                    this.PANEL_HEIGHT = 200;
                    this.PANEL_X = 260;
                    this.PANEL_Y = 250;
                }
            },
            methods: {
                GetAIHackMessageFrame: function (globalState, sessionState) {
                    return new ChessCompStompWithHacksLibrary.AIMessageFrame(globalState, sessionState, new ChessCompStompWithHacksLibrary.ChessFrame(globalState, sessionState), "If you're going to hack, then I'm hacking too!", 29, 114);
                },
                GetFinalBattleMessageFrame: function (globalState, sessionState) {
                    return new ChessCompStompWithHacksLibrary.AIMessageFrame(globalState, sessionState, new ChessCompStompWithHacksLibrary.ChessFrame(globalState, sessionState), "I have 23 queens. Good luck; have fun!  :)", 50, 114);
                }
            }
        },
        fields: {
            globalState: null,
            sessionState: null,
            underlyingFrame: null,
            message: null,
            messageXOffset: 0,
            messageYOffset: 0,
            confirmButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState, underlyingFrame, message, messageXOffset, messageYOffset) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.underlyingFrame = underlyingFrame;
                this.message = message;
                this.messageXOffset = messageXOffset;
                this.messageYOffset = messageYOffset;

                var buttonWidth = 150;

                this.confirmButton = new ChessCompStompWithHacksLibrary.Button(((ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_X + ((Bridge.Int.div((((ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_WIDTH - buttonWidth) | 0)), 2)) | 0)) | 0), 270, buttonWidth, 40, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "OK", 57, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                var isConfirmClicked = this.confirmButton.ProcessFrame(mouseInput, previousMouseInput);

                if (isConfirmClicked) {
                    return this.underlyingFrame;
                }

                return this;
            },
            ProcessMusic: function () {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic();
            },
            Render: function (displayOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render(displayOutput);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.$ctor1(0, 0, 0, 64), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_X, ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_Y, 479, 199, DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_X, ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_Y, ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_WIDTH, ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_HEIGHT, DTLibrary.DTColor.Black(), false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(364, 422, "Message from the AI", ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_X + this.messageXOffset) | 0), ((ChessCompStompWithHacksLibrary.AIMessageFrame.PANEL_Y + this.messageYOffset) | 0), this.message, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, DTLibrary.DTColor.Black());

                this.confirmButton.Render(displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        statics: {
            fields: {
                GAME_LOGIC_X_OFFSET: 0,
                GAME_LOGIC_Y_OFFSET: 0
            },
            ctors: {
                init: function () {
                    this.GAME_LOGIC_X_OFFSET = 0;
                    this.GAME_LOGIC_Y_OFFSET = 50;
                }
            }
        },
        fields: {
            globalState: null,
            sessionState: null,
            delayBeforeShowingPanel: null,
            victoryStalemateOrDefeatPanel: null,
            finalBattleVictoryPanel: null,
            settingsIcon: null,
            resignButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.delayBeforeShowingPanel = null;
                this.victoryStalemateOrDefeatPanel = null;
                this.finalBattleVictoryPanel = null;

                this.settingsIcon = new ChessCompStompWithHacksLibrary.SettingsIcon();

                this.resignButton = new ChessCompStompWithHacksLibrary.Button(869, 100, 100, 40, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Resign", 14, 9, ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) {
                var gameLogic = this.sessionState.GetGameLogic();
                if (gameLogic != null) {
                    gameLogic.ProcessExtraTime(milliseconds);
                }
            },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                var victoryStalemateOrDefeatPanelResult = null;

                if (this.victoryStalemateOrDefeatPanel != null) {
                    victoryStalemateOrDefeatPanelResult = this.victoryStalemateOrDefeatPanel.ProcessFrame(mouseInput, previousMouseInput);
                    if (victoryStalemateOrDefeatPanelResult.HasClickedContinueButton) {
                        return new ChessCompStompWithHacksLibrary.HackSelectionScreenFrame(this.globalState, this.sessionState);
                    }
                }

                var finalBattleVictoryPanelResult = null;

                if (this.finalBattleVictoryPanel != null) {
                    finalBattleVictoryPanelResult = this.finalBattleVictoryPanel.ProcessFrame(mouseInput, previousMouseInput);
                    if (finalBattleVictoryPanelResult.HasClickedContinueButton) {
                        return new ChessCompStompWithHacksLibrary.TitleScreenFrame(this.globalState, this.sessionState);
                    }
                }

                var isHoverOverPanel = victoryStalemateOrDefeatPanelResult != null && victoryStalemateOrDefeatPanelResult.IsHoverOverPanel || finalBattleVictoryPanelResult != null && finalBattleVictoryPanelResult.IsHoverOverPanel;

                var dummyMouseInput = new DTLibrary.SimulatedMouse(-999, -999, mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed(), mouseInput.DTLibrary$IMouse$IsRightMouseButtonPressed());

                var gameLogic = this.sessionState.GetGameLogic();
                if (gameLogic == null) {
                    gameLogic = this.sessionState.GetMostRecentGameLogic();
                }
                var result = gameLogic.ProcessNextFrame(isHoverOverPanel ? dummyMouseInput : new DTLibrary.TranslatedMouse(mouseInput, 0, -50), displayProcessing, soundOutput, this.globalState.ElapsedMicrosPerFrame);

                this.sessionState.AddCompletedObjectives(new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Objective)).$ctor1(result.CompletedObjectives));

                var didPlayerWin = result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.WhiteVictory && result.IsPlayerWhite || result.GameStatus === ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.BlackVictory && !result.IsPlayerWhite;

                if (this.delayBeforeShowingPanel == null) {
                    if (result.GameStatus !== ChessCompStompWithHacksEngine.ComputeMoves.GameStatus.InProgress) {
                        this.sessionState.CompleteGame(didPlayerWin);
                        this.delayBeforeShowingPanel = 0;
                    }
                }

                if (this.delayBeforeShowingPanel != null && this.victoryStalemateOrDefeatPanel == null && this.finalBattleVictoryPanel == null) {
                    this.delayBeforeShowingPanel = Bridge.Int.clip32(System.Nullable.getValue(this.delayBeforeShowingPanel) + this.globalState.ElapsedMicrosPerFrame);

                    if (System.Nullable.getValue(this.delayBeforeShowingPanel) >= 1000000) {
                        if (didPlayerWin && result.IsFinalBattle && !this.sessionState.HasShownFinalBattleVictoryPanel()) {
                            this.sessionState.SetShownFinalBattleVictoryPanel();
                            this.finalBattleVictoryPanel = new ChessCompStompWithHacksLibrary.FinalBattleVictoryPanel();
                        } else {
                            this.victoryStalemateOrDefeatPanel = new ChessCompStompWithHacksLibrary.VictoryStalemateOrDefeatPanel(result.GameStatus, result.IsPlayerWhite);
                        }
                    }
                }

                var isWaitingForFinalBattleVictoryPanel = didPlayerWin && result.IsFinalBattle && !this.sessionState.HasShownFinalBattleVictoryPanel() && this.finalBattleVictoryPanel == null;

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !isWaitingForFinalBattleVictoryPanel) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, true);
                }

                if (this.delayBeforeShowingPanel == null) {
                    var hasClickedResignButton = this.resignButton.ProcessFrame(mouseInput, previousMouseInput);

                    if (hasClickedResignButton) {
                        return new ChessCompStompWithHacksLibrary.ResignConfirmationFrame(this.globalState, this.sessionState, this);
                    }
                }

                var hasClicked = this.settingsIcon.ProcessFrame(mouseInput, previousMouseInput, isHoverOverPanel, displayProcessing);

                if (hasClicked && !isWaitingForFinalBattleVictoryPanel) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, true);
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.ctor(223, 220, 217), true);

                var gameLogic = this.sessionState.GetGameLogic();
                if (gameLogic == null) {
                    gameLogic = this.sessionState.GetMostRecentGameLogic();
                }
                gameLogic.Render(new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, ChessCompStompWithHacksLibrary.ChessFrame.GAME_LOGIC_X_OFFSET, ChessCompStompWithHacksLibrary.ChessFrame.GAME_LOGIC_Y_OFFSET));

                if (this.delayBeforeShowingPanel == null) {
                    this.resignButton.Render(displayOutput);
                }

                this.settingsIcon.Render(displayOutput);

                if (this.victoryStalemateOrDefeatPanel != null) {
                    this.victoryStalemateOrDefeatPanel.Render(displayOutput);
                }

                if (this.finalBattleVictoryPanel != null) {
                    this.finalBattleVictoryPanel.Render(displayOutput);
                }
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ChessTestingFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) { },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        statics: {
            fields: {
                PANEL_WIDTH: 0,
                PANEL_HEIGHT: 0,
                PANEL_X: 0,
                PANEL_Y: 0
            },
            ctors: {
                init: function () {
                    this.PANEL_WIDTH = 480;
                    this.PANEL_HEIGHT = 150;
                    this.PANEL_X = 260;
                    this.PANEL_Y = 275;
                }
            }
        },
        fields: {
            globalState: null,
            sessionState: null,
            underlyingFrame: null,
            confirmButton: null,
            cancelButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState, underlyingFrame) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.underlyingFrame = underlyingFrame;

                var buttonWidth = 150;
                var buttonHeight = 40;

                this.confirmButton = new ChessCompStompWithHacksLibrary.Button(340, 295, buttonWidth, buttonHeight, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Yes", 47, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.cancelButton = new ChessCompStompWithHacksLibrary.Button(510, 295, buttonWidth, buttonHeight, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "No", 55, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return this.underlyingFrame;
                }

                var isConfirmClicked = this.confirmButton.ProcessFrame(mouseInput, previousMouseInput);

                var isCancelClicked = this.cancelButton.ProcessFrame(mouseInput, previousMouseInput);

                if (isConfirmClicked) {
                    this.sessionState.ClearData();
                    return this.underlyingFrame;
                }

                if (isCancelClicked) {
                    return this.underlyingFrame;
                }

                return this;
            },
            ProcessMusic: function () {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic();
            },
            Render: function (displayOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render(displayOutput);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.$ctor1(0, 0, 0, 64), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame.PANEL_X, ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame.PANEL_Y, 479, 149, DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame.PANEL_X, ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame.PANEL_Y, ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame.PANEL_WIDTH, ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame.PANEL_HEIGHT, DTLibrary.DTColor.Black(), false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(287, 407, "Are you sure you want to reset\nyour progress?", ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());

                this.confirmButton.Render(displayOutput);
                this.cancelButton.Render(displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.CreditsFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null,
            tabButtons: null,
            selectedTab: 0,
            hoverTab: null,
            clickTab: null,
            backButton: null
        },
        alias: [
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;

                this.selectedTab = ChessCompStompWithHacksLibrary.CreditsFrame.Tab.DesignAndCoding;
                this.hoverTab = null;
                this.clickTab = null;

                this.tabButtons = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.CreditsFrame.TabButton)).ctor();
                this.tabButtons.add(new ChessCompStompWithHacksLibrary.CreditsFrame.TabButton(20, 569, 234, 40, ChessCompStompWithHacksLibrary.CreditsFrame.Tab.DesignAndCoding, "Design and coding"));
                this.tabButtons.add(new ChessCompStompWithHacksLibrary.CreditsFrame.TabButton(254, 569, 103, 40, ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Images, "Images"));
                this.tabButtons.add(new ChessCompStompWithHacksLibrary.CreditsFrame.TabButton(357, 569, 82, 40, ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Font, "Font"));

                if (this.globalState.ShowSoundAndMusicVolumePicker) {
                    this.tabButtons.add(new ChessCompStompWithHacksLibrary.CreditsFrame.TabButton(439, 569, 96, 40, ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Sound, "Sound"));
                    this.tabButtons.add(new ChessCompStompWithHacksLibrary.CreditsFrame.TabButton(535, 569, 90, 40, ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Music, "Music"));
                }

                this.backButton = new ChessCompStompWithHacksLibrary.Button(780, 20, 200, 80, new DTLibrary.DTColor.ctor(235, 235, 235), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Back", 67, 28, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                var $t;
                var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                this.hoverTab = null;
                $t = Bridge.getEnumerator(this.tabButtons);
                try {
                    while ($t.moveNext()) {
                        var tabButton = $t.Current;
                        if (tabButton.X <= mouseX && mouseX <= ((tabButton.X + tabButton.Width) | 0) && tabButton.Y <= mouseY && mouseY <= ((tabButton.Y + tabButton.Height) | 0)) {
                            this.hoverTab = tabButton.Tab;
                        }
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    if (this.hoverTab != null) {
                        this.clickTab = this.hoverTab;
                    }
                }

                if (this.clickTab != null && !mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    if (System.Nullable.hasValue(this.hoverTab) && System.Nullable.getValue(this.hoverTab) === System.Nullable.getValue(this.clickTab)) {
                        this.selectedTab = System.Nullable.getValue(this.clickTab);
                    }

                    this.clickTab = null;
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.TitleScreenFrame(this.globalState, this.sessionState);
                }

                var clickedBackButton = this.backButton.ProcessFrame(mouseInput, previousMouseInput);
                if (clickedBackButton) {
                    return new ChessCompStompWithHacksLibrary.TitleScreenFrame(this.globalState, this.sessionState);
                }

                return this;
            },
            ProcessExtraTime: function (milliseconds) { },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                var $t;
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.ctor(223, 220, 217), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(422, 675, "Credits", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(20, 120, 959, 449, DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(20, 120, 960, 450, DTLibrary.DTColor.Black(), false);

                $t = Bridge.getEnumerator(this.tabButtons);
                try {
                    while ($t.moveNext()) {
                        var tabButton = $t.Current;
                        var backgroundColor;

                        if (tabButton.Tab === this.selectedTab) {
                            backgroundColor = DTLibrary.DTColor.White();
                        } else {
                            if (System.Nullable.hasValue(this.clickTab) && System.Nullable.getValue(this.clickTab) === tabButton.Tab) {
                                backgroundColor = new DTLibrary.DTColor.ctor(252, 251, 154);
                            } else {
                                if (System.Nullable.hasValue(this.hoverTab) && System.Nullable.getValue(this.hoverTab) === tabButton.Tab) {
                                    backgroundColor = new DTLibrary.DTColor.ctor(250, 249, 200);
                                } else {
                                    backgroundColor = new DTLibrary.DTColor.ctor(200, 200, 200);
                                }
                            }
                        }

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(tabButton.X, tabButton.Y, ((tabButton.Width - 1) | 0), ((tabButton.Height - 1) | 0), backgroundColor, true);

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(tabButton.X, tabButton.Y, tabButton.Width, tabButton.Height, DTLibrary.DTColor.Black(), false);

                        if (this.selectedTab === tabButton.Tab) {
                            displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((tabButton.X + 1) | 0), ((tabButton.Y - 1) | 0), ((tabButton.Width - 2) | 0), 3, DTLibrary.DTColor.White(), true);
                        }

                        displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(((tabButton.X + 10) | 0), ((((tabButton.Y + tabButton.Height) | 0) - 10) | 0), tabButton.TabName, ChessCompStompWithHacksLibrary.ChessFont.Fetamont18Pt, DTLibrary.DTColor.Black());
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                var translatedDisplay = new (DTLibrary.TranslatedDisplayOutput$2(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont))(displayOutput, 20, 120);

                if (this.selectedTab === ChessCompStompWithHacksLibrary.CreditsFrame.Tab.DesignAndCoding) {
                    ChessCompStompWithHacksLibrary.Credits_DesignAndCoding.Render(translatedDisplay, 960, 450, this.globalState.IsWebBrowserVersion);
                }
                if (this.selectedTab === ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Images) {
                    ChessCompStompWithHacksLibrary.Credits_Images.Render(translatedDisplay, 960, 450);
                }
                if (this.selectedTab === ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Font) {
                    ChessCompStompWithHacksLibrary.Credits_Font.Render(translatedDisplay, 960, 450);
                }
                if (this.selectedTab === ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Sound) {
                    ChessCompStompWithHacksLibrary.Credits_Sound.Render(translatedDisplay, 960, 450);
                }
                if (this.selectedTab === ChessCompStompWithHacksLibrary.CreditsFrame.Tab.Music) {
                    ChessCompStompWithHacksLibrary.Credits_Music.Render(translatedDisplay, 960, 450);
                }

                this.backButton.Render(displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.HackSelectionScreenFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null,
            settingsIcon: null,
            hackDisplays: null,
            resetHacksButton: null,
            continueButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;

                this.settingsIcon = new ChessCompStompWithHacksLibrary.SettingsIcon();

                var hacks = function (_o1) {
                        _o1.add(ChessCompStompWithHacksEngine.Hack.ExtraPawnFirst);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.ExtraPawnSecond);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.PawnsCanMoveThreeSpacesInitially);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.SuperCastling);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.StalemateIsVictory);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.KnightsCanMakeLargeKnightsMove);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.RooksCanMoveLikeBishops);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.RooksCanCaptureLikeCannons);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.ExtraQueen);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.QueensCanMoveLikeKnights);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.SuperEnPassant);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.AnyPieceCanPromote);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.OpponentMustCaptureWhenPossible);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.PawnsDestroyCapturingPiece);
                        _o1.add(ChessCompStompWithHacksEngine.Hack.TacticalNuke);
                        return _o1;
                    }(new (System.Collections.Generic.List$1(ChessCompStompWithHacksEngine.Hack)).ctor());

                this.hackDisplays = new (System.Collections.Generic.List$1(ChessCompStompWithHacksLibrary.HackDisplay)).ctor();

                for (var i = 0; i < 3; i = (i + 1) | 0) {
                    for (var j = 0; j < 5; j = (j + 1) | 0) {
                        var hack = hacks.getItem(0);
                        hacks.removeAt(0);

                        this.hackDisplays.add(new ChessCompStompWithHacksLibrary.HackDisplay(hack, ((Bridge.Int.mul(198, j) + 8) | 0), ((450 - Bridge.Int.mul(115, i)) | 0), this.sessionState));
                    }
                }

                this.resetHacksButton = new ChessCompStompWithHacksLibrary.Button(8, 70, 170, 40, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Reset hacks", 18, 9, ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt);

                this.continueButton = new ChessCompStompWithHacksLibrary.Button(700, 50, 200, 80, new DTLibrary.DTColor.ctor(235, 235, 235), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Continue", 40, 27, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                var $t;
                if (this.globalState.DebugMode) {
                    if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.One) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.One)) {
                        this.sessionState.AddCompletedObjectives(function (_o1) {
                                _o1.add(ChessCompStompWithHacksEngine.Objective.DefeatComputer);
                                return _o1;
                            }(new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Objective)).ctor()));
                        this.sessionState.Debug_AddWin();
                    }

                    if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Two) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Two)) {
                        this.sessionState.AddCompletedObjectives(function (_o2) {
                                _o2.add(ChessCompStompWithHacksEngine.Objective.DefeatComputer);
                                _o2.add(ChessCompStompWithHacksEngine.Objective.DefeatComputerByPlayingAtMost25Moves);
                                _o2.add(ChessCompStompWithHacksEngine.Objective.DefeatComputerWith5QueensOnTheBoard);
                                _o2.add(ChessCompStompWithHacksEngine.Objective.CheckmateUsingAKnight);
                                _o2.add(ChessCompStompWithHacksEngine.Objective.PromoteAPieceToABishop);
                                _o2.add(ChessCompStompWithHacksEngine.Objective.LaunchANuke);
                                return _o2;
                            }(new (System.Collections.Generic.HashSet$1(ChessCompStompWithHacksEngine.Objective)).ctor()));
                    }
                }

                $t = Bridge.getEnumerator(this.hackDisplays);
                try {
                    while ($t.moveNext()) {
                        var hackDisplay = $t.Current;
                        hackDisplay.ProcessFrame(mouseInput, previousMouseInput, displayProcessing);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                var clickedContinueButton = this.continueButton.ProcessFrame(mouseInput, previousMouseInput);
                if (clickedContinueButton) {
                    return new ChessCompStompWithHacksLibrary.ObjectivesScreenFrame(this.globalState, this.sessionState);
                }

                var clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput, previousMouseInput, false, displayProcessing);
                if (clickedSettingsIcon) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, false);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, false);
                }

                var clickedResetHacksButton = this.resetHacksButton.ProcessFrame(mouseInput, previousMouseInput);
                if (clickedResetHacksButton) {
                    this.sessionState.ResetResearchedHacks();
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                var $t, $t1;
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.ctor(223, 220, 217), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(436, 675, "Hacks", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(250, 122, "Hack points remaining: " + (DTLibrary.StringUtil.ToStringCultureInvariant(this.sessionState.GetUnusedHackPoints()) || "") + "\n" + "Get more hack points by winning games" + "\n" + "and completing objectives!", ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt, DTLibrary.DTColor.Black());

                $t = Bridge.getEnumerator(this.hackDisplays);
                try {
                    while ($t.moveNext()) {
                        var hackDisplay = $t.Current;
                        hackDisplay.RenderButtonDisplay(displayOutput);
                    }
                } finally {
                    if (Bridge.is($t, System.IDisposable)) {
                        $t.System$IDisposable$Dispose();
                    }
                }

                this.resetHacksButton.Render(displayOutput);

                this.settingsIcon.Render(displayOutput);

                this.continueButton.Render(displayOutput);

                $t1 = Bridge.getEnumerator(this.hackDisplays);
                try {
                    while ($t1.moveNext()) {
                        var hackDisplay1 = $t1.Current;
                        hackDisplay1.RenderHoverDisplay(displayOutput);
                    }
                } finally {
                    if (Bridge.is($t1, System.IDisposable)) {
                        $t1.System$IDisposable$Dispose();
                    }
                }
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.InitialLoadingScreenFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState) {
                this.$initialize();
                this.globalState = globalState;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                var returnValue = this.GetNextFrameHelper(displayProcessing, soundOutput, musicProcessing);

                if (returnValue != null) {
                    return returnValue;
                }

                returnValue = this.GetNextFrameHelper(displayProcessing, soundOutput, musicProcessing);

                if (returnValue != null) {
                    return returnValue;
                }

                return this;
            },
            GetNextFrameHelper: function (displayProcessing, soundOutput, musicProcessing) {
                var isDoneLoadingImages = displayProcessing.DTLibrary$IDisplayProcessing$1$ChessCompStompWithHacksLibrary$ChessImage$LoadImages();

                if (!isDoneLoadingImages) {
                    return null;
                }

                var isDoneLoadingSounds = soundOutput.DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$LoadSounds();

                if (!isDoneLoadingSounds) {
                    return null;
                }

                var isDoneLoadingMusic = musicProcessing.DTLibrary$IMusicProcessing$LoadMusic();

                if (!isDoneLoadingMusic) {
                    return null;
                }

                return new ChessCompStompWithHacksLibrary.TitleScreenFrame(this.globalState, new ChessCompStompWithHacksLibrary.SessionState(this.globalState.Timer));
            },
            ProcessMusic: function () { },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawInitialLoadingScreen();
            },
            RenderMusic: function (musicOutput) { }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.IntroScreenFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        statics: {
            fields: {
                TOTAL_TIME_TO_DISPLAY_TEXT: 0
            },
            ctors: {
                init: function () {
                    this.TOTAL_TIME_TO_DISPLAY_TEXT = 4000000;
                }
            }
        },
        fields: {
            globalState: null,
            sessionState: null,
            settingsIcon: null,
            continueButton: null,
            elapsedTimeMicros: 0
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.settingsIcon = new ChessCompStompWithHacksLibrary.SettingsIcon();
                this.elapsedTimeMicros = 0;

                var buttonWidth = 150;
                this.continueButton = new ChessCompStompWithHacksLibrary.Button(((Bridge.Int.div((((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH - buttonWidth) | 0)), 2)) | 0), 300, buttonWidth, 50, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Begin", 38, 13, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                this.elapsedTimeMicros = (this.elapsedTimeMicros + this.globalState.ElapsedMicrosPerFrame) | 0;
                if (this.elapsedTimeMicros >= ChessCompStompWithHacksLibrary.IntroScreenFrame.TOTAL_TIME_TO_DISPLAY_TEXT) {
                    this.elapsedTimeMicros = 4000001;
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, false);
                }

                var clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput, previousMouseInput, false, displayProcessing);
                if (clickedSettingsIcon) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, false);
                }

                if (this.elapsedTimeMicros >= ChessCompStompWithHacksLibrary.IntroScreenFrame.TOTAL_TIME_TO_DISPLAY_TEXT) {
                    var clickedContinueButton = this.continueButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedContinueButton) {
                        this.sessionState.StartNewGame();
                        return new ChessCompStompWithHacksLibrary.HackSelectionScreenFrame(this.globalState, this.sessionState);
                    }
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Space) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Space) || keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Enter) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Enter) || mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !clickedSettingsIcon) {
                    this.elapsedTimeMicros = 4000001;
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.ctor(223, 220, 217), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(406, 675, "Welcome", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                var text = "Today, you're playing against a Powerful Chess AI.\nYou are not a great chess player but you are an Elite Hacker.\nUse your elite hacking skills to defeat the AI.";

                var index;
                if (this.elapsedTimeMicros >= ChessCompStompWithHacksLibrary.IntroScreenFrame.TOTAL_TIME_TO_DISPLAY_TEXT) {
                    index = text.length;
                } else {
                    index = System.Int64.clip32(System.Int64(this.elapsedTimeMicros).mul(System.Int64(text.length)).div((System.Int64(4000000))));
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(100, 500, index >= text.length ? text : text.substr(0, index), ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());

                if (this.elapsedTimeMicros >= ChessCompStompWithHacksLibrary.IntroScreenFrame.TOTAL_TIME_TO_DISPLAY_TEXT) {
                    this.continueButton.Render(displayOutput);
                }

                this.settingsIcon.Render(displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ObjectivesScreenFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null,
            completedObjectives: null,
            hasUnlockedFinalObjective: false,
            settingsIcon: null,
            backToHackSelectionFrameButton: null,
            startNextGameButton_finalBattleNotUnlocked: null,
            startFinalBattleButton: null,
            startNonFinalBattleButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                var completedObjectives = sessionState.GetCompletedObjectives();
                this.completedObjectives = completedObjectives;
                this.hasUnlockedFinalObjective = ChessCompStompWithHacksLibrary.ObjectiveDisplay.HasUnlockedFinalObjective(completedObjectives);

                this.settingsIcon = new ChessCompStompWithHacksLibrary.SettingsIcon();

                this.backToHackSelectionFrameButton = new ChessCompStompWithHacksLibrary.Button(62, 70, 100, 40, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Back", 22, 9, ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt);

                this.startNextGameButton_finalBattleNotUnlocked = new ChessCompStompWithHacksLibrary.Button(300, 50, 400, 80, new DTLibrary.DTColor.ctor(235, 235, 235), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Start next round", 84, 27, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.startFinalBattleButton = new ChessCompStompWithHacksLibrary.Button(300, 80, 400, 80, new DTLibrary.DTColor.ctor(235, 235, 235), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Start the Final Battle!", 46, 27, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.startNonFinalBattleButton = new ChessCompStompWithHacksLibrary.Button(300, 50, 400, 31, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Start a regular game", 83, 7, ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                var clickedSettingsIcon = this.settingsIcon.ProcessFrame(mouseInput, previousMouseInput, false, displayProcessing);
                if (clickedSettingsIcon) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, false);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.SettingsMenuFrame(this.globalState, this.sessionState, this, false);
                }

                var clickedBackButton = this.backToHackSelectionFrameButton.ProcessFrame(mouseInput, previousMouseInput);
                if (clickedBackButton) {
                    return new ChessCompStompWithHacksLibrary.HackSelectionScreenFrame(this.globalState, this.sessionState);
                }

                if (this.hasUnlockedFinalObjective) {
                    var clickedStartFinalBattleButton = this.startFinalBattleButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedStartFinalBattleButton) {
                        return this.sessionState.StartGame(true, this.globalState);
                    }

                    var clickedStartNonFinalBattleButton = this.startNonFinalBattleButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedStartNonFinalBattleButton) {
                        return this.sessionState.StartGame(false, this.globalState);
                    }
                } else {
                    var clickedStartGameButton = this.startNextGameButton_finalBattleNotUnlocked.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedStartGameButton) {
                        return this.sessionState.StartGame(false, this.globalState);
                    }
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.ctor(223, 220, 217), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(389, 675, "Objectives", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                this.settingsIcon.Render(displayOutput);

                this.backToHackSelectionFrameButton.Render(displayOutput);

                if (this.hasUnlockedFinalObjective) {
                    this.startFinalBattleButton.Render(displayOutput);
                    this.startNonFinalBattleButton.Render(displayOutput);
                } else {
                    this.startNextGameButton_finalBattleNotUnlocked.Render(displayOutput);
                }

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderNonFinalObjective(62, 450, ChessCompStompWithHacksEngine.Objective.DefeatComputer, this.completedObjectives, displayOutput);

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderNonFinalObjective(375, 450, ChessCompStompWithHacksEngine.Objective.DefeatComputerByPlayingAtMost25Moves, this.completedObjectives, displayOutput);

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderNonFinalObjective(687, 450, ChessCompStompWithHacksEngine.Objective.DefeatComputerWith5QueensOnTheBoard, this.completedObjectives, displayOutput);

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderNonFinalObjective(62, 320, ChessCompStompWithHacksEngine.Objective.CheckmateUsingAKnight, this.completedObjectives, displayOutput);

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderNonFinalObjective(375, 320, ChessCompStompWithHacksEngine.Objective.PromoteAPieceToABishop, this.completedObjectives, displayOutput);

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderNonFinalObjective(687, 320, ChessCompStompWithHacksEngine.Objective.LaunchANuke, this.completedObjectives, displayOutput);

                ChessCompStompWithHacksLibrary.ObjectiveDisplay.RenderFinalObjective(250, 190, this.completedObjectives, displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.ResignConfirmationFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        statics: {
            fields: {
                PANEL_WIDTH: 0,
                PANEL_HEIGHT: 0,
                PANEL_X: 0,
                PANEL_Y: 0
            },
            ctors: {
                init: function () {
                    this.PANEL_WIDTH = 480;
                    this.PANEL_HEIGHT = 150;
                    this.PANEL_X = 260;
                    this.PANEL_Y = 275;
                }
            }
        },
        fields: {
            globalState: null,
            sessionState: null,
            underlyingFrame: null,
            confirmButton: null,
            cancelButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState, underlyingFrame) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.underlyingFrame = underlyingFrame;

                var buttonWidth = 150;
                var buttonHeight = 40;

                this.confirmButton = new ChessCompStompWithHacksLibrary.Button(340, 295, buttonWidth, buttonHeight, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Yes", 47, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.cancelButton = new ChessCompStompWithHacksLibrary.Button(510, 295, buttonWidth, buttonHeight, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "No", 55, 8, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return this.underlyingFrame;
                }

                var isConfirmClicked = this.confirmButton.ProcessFrame(mouseInput, previousMouseInput);

                var isCancelClicked = this.cancelButton.ProcessFrame(mouseInput, previousMouseInput);

                if (isConfirmClicked) {
                    this.sessionState.CompleteGame(false);
                    return new ChessCompStompWithHacksLibrary.HackSelectionScreenFrame(this.globalState, this.sessionState);
                }

                if (isCancelClicked) {
                    return this.underlyingFrame;
                }

                return this;
            },
            ProcessMusic: function () {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic();
            },
            Render: function (displayOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render(displayOutput);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.$ctor1(0, 0, 0, 64), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.ResignConfirmationFrame.PANEL_X, ChessCompStompWithHacksLibrary.ResignConfirmationFrame.PANEL_Y, 479, 149, DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.ResignConfirmationFrame.PANEL_X, ChessCompStompWithHacksLibrary.ResignConfirmationFrame.PANEL_Y, ChessCompStompWithHacksLibrary.ResignConfirmationFrame.PANEL_WIDTH, ChessCompStompWithHacksLibrary.ResignConfirmationFrame.PANEL_HEIGHT, DTLibrary.DTColor.Black(), false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(276, 397, "Are you sure you want to resign?", ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());

                this.confirmButton.Render(displayOutput);
                this.cancelButton.Render(displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.SettingsMenuFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        statics: {
            fields: {
                PANEL_WIDTH: 0,
                PANEL_HEIGHT_WITH_PAUSE: 0,
                PANEL_HEIGHT_WITHOUT_PAUSE: 0,
                PANEL_HEIGHT_WITH_PAUSE_WITHOUT_VOLUME_CONTROL: 0,
                PANEL_HEIGHT_WITHOUT_PAUSE_WITHOUT_VOLUME_CONTROL: 0,
                PANEL_X: 0,
                PANEL_Y_WITH_PAUSE: 0,
                PANEL_Y_WITHOUT_PAUSE: 0,
                PANEL_Y_WITH_PAUSE_WITHOUT_VOLUME_CONTROL: 0,
                PANEL_Y_WITHOUT_PAUSE_WITHOUT_VOLUME_CONTROL: 0,
                BUTTON_WIDTH: 0,
                BUTTON_HEIGHT: 0
            },
            ctors: {
                init: function () {
                    this.PANEL_WIDTH = 300;
                    this.PANEL_HEIGHT_WITH_PAUSE = 380;
                    this.PANEL_HEIGHT_WITHOUT_PAUSE = 263;
                    this.PANEL_HEIGHT_WITH_PAUSE_WITHOUT_VOLUME_CONTROL = 257;
                    this.PANEL_HEIGHT_WITHOUT_PAUSE_WITHOUT_VOLUME_CONTROL = 140;
                    this.PANEL_X = 350;
                    this.PANEL_Y_WITH_PAUSE = 160;
                    this.PANEL_Y_WITHOUT_PAUSE = 218;
                    this.PANEL_Y_WITH_PAUSE_WITHOUT_VOLUME_CONTROL = 221;
                    this.PANEL_Y_WITHOUT_PAUSE_WITHOUT_VOLUME_CONTROL = 280;
                    this.BUTTON_WIDTH = 240;
                    this.BUTTON_HEIGHT = 40;
                }
            }
        },
        fields: {
            globalState: null,
            sessionState: null,
            soundAndMusicVolumePicker: null,
            underlyingFrame: null,
            continueButton: null,
            backToTitleScreenButton: null,
            showPausedText: false,
            panelY: 0,
            panelHeight: 0
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState, underlyingFrame, showPausedText) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.soundAndMusicVolumePicker = null;
                this.underlyingFrame = underlyingFrame;

                this.showPausedText = showPausedText;

                if (globalState.ShowSoundAndMusicVolumePicker) {
                    this.panelY = showPausedText ? ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_Y_WITH_PAUSE : ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_Y_WITHOUT_PAUSE;
                    this.panelHeight = showPausedText ? ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_HEIGHT_WITH_PAUSE : ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_HEIGHT_WITHOUT_PAUSE;
                } else {
                    this.panelY = showPausedText ? ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_Y_WITH_PAUSE_WITHOUT_VOLUME_CONTROL : ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_Y_WITHOUT_PAUSE_WITHOUT_VOLUME_CONTROL;
                    this.panelHeight = showPausedText ? ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_HEIGHT_WITH_PAUSE_WITHOUT_VOLUME_CONTROL : ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_HEIGHT_WITHOUT_PAUSE_WITHOUT_VOLUME_CONTROL;
                }

                this.continueButton = new ChessCompStompWithHacksLibrary.Button(380, ((this.panelY + 80) | 0), ChessCompStompWithHacksLibrary.SettingsMenuFrame.BUTTON_WIDTH, ChessCompStompWithHacksLibrary.SettingsMenuFrame.BUTTON_HEIGHT, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Continue", 76, 12, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt);

                this.backToTitleScreenButton = new ChessCompStompWithHacksLibrary.Button(380, ((this.panelY + 20) | 0), ChessCompStompWithHacksLibrary.SettingsMenuFrame.BUTTON_WIDTH, ChessCompStompWithHacksLibrary.SettingsMenuFrame.BUTTON_HEIGHT, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Return to title screen", 11, 12, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (this.soundAndMusicVolumePicker == null) {
                    this.soundAndMusicVolumePicker = new ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker(380, ((this.panelY + (this.showPausedText ? 170 : 140)) | 0), soundOutput.DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$GetSoundVolume(), this.globalState.MusicVolume, this.globalState.ElapsedMicrosPerFrame);
                }

                if (this.globalState.ShowSoundAndMusicVolumePicker) {
                    this.soundAndMusicVolumePicker.ProcessFrame(mouseInput, previousMouseInput);
                    soundOutput.DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$SetSoundVolume(this.soundAndMusicVolumePicker.GetCurrentSoundVolume());
                    this.globalState.MusicVolume = this.soundAndMusicVolumePicker.GetCurrentMusicVolume();
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return this.underlyingFrame;
                }

                var clickedContinueButton = this.continueButton.ProcessFrame(mouseInput, previousMouseInput);

                if (clickedContinueButton) {
                    return this.underlyingFrame;
                }

                var clickedBackToTitleScreenButton = this.backToTitleScreenButton.ProcessFrame(mouseInput, previousMouseInput);

                if (clickedBackToTitleScreenButton) {
                    return new ChessCompStompWithHacksLibrary.TitleScreenFrame(this.globalState, this.sessionState);
                }

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    var mouseX = mouseInput.DTLibrary$IMouse$GetX();
                    var mouseY = mouseInput.DTLibrary$IMouse$GetY();

                    if (mouseX < ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_X || mouseX > 650 || mouseY < this.panelY || mouseY > ((this.panelY + this.panelHeight) | 0)) {
                        return this.underlyingFrame;
                    }
                }

                return this;
            },
            ProcessMusic: function () {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic();
            },
            Render: function (displayOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render(displayOutput);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.$ctor1(0, 0, 0, 64), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_X, this.panelY, 299, ((this.panelHeight - 1) | 0), DTLibrary.DTColor.White(), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_X, this.panelY, ChessCompStompWithHacksLibrary.SettingsMenuFrame.PANEL_WIDTH, this.panelHeight, DTLibrary.DTColor.Black(), false);

                if (this.soundAndMusicVolumePicker != null) {
                    if (this.globalState.ShowSoundAndMusicVolumePicker) {
                        this.soundAndMusicVolumePicker.Render(displayOutput);
                    }
                }

                if (this.showPausedText) {
                    displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(422, ((this.panelY + (this.globalState.ShowSoundAndMusicVolumePicker ? 362 : 239)) | 0), "Paused", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());
                }

                this.continueButton.Render(displayOutput);
                this.backToTitleScreenButton.Render(displayOutput);
            },
            RenderMusic: function (musicOutput) {
                this.underlyingFrame.DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.TestingFontFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.TestingFrame(this.globalState, this.sessionState);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Enter) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Enter)) {
                    return new ChessCompStompWithHacksLibrary.TestingFontFrame2(this.globalState, this.sessionState);
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                var red = new DTLibrary.DTColor.ctor(255, 0, 0);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(51, 590, 836, 60, red, false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 650, "Line 1 ABCDEFGHIJKLMNOPQRSTUVWXYZ ABCDEFGHIJKLMNOPQRSTUVWXYZ ABCDEFGHIJKLMNOPQRSTUVWXYZ\nLine 2\nLine 3\nLine 4", ChessCompStompWithHacksLibrary.ChessFont.Fetamont12Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(51, 479, 632, 71, red, false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 550, "Line 1 abcdefghijklmnopqrstuvwxyz abcdefghijklmnopqrstuvwxyz\nLine 2\nLine 3\nLine 4", ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(51, 364, 714, 85, red, false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 450, "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks\nLine 2\nLine 3\nLine 4", ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(51, 259, 790, 90, red, false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 350, "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks\nLine 2\nLine 3\nLine 4", ChessCompStompWithHacksLibrary.ChessFont.Fetamont18Pt, DTLibrary.DTColor.Black());

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(51, 144, 874, 104, red, false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 250, "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks\nLine 2\nLine 3\nLine 4", ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt, DTLibrary.DTColor.Black());
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.TestingFontFrame2", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.TestingFrame(this.globalState, this.sessionState);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Enter) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Enter)) {
                    return new ChessCompStompWithHacksLibrary.TestingFontFrame(this.globalState, this.sessionState);
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                var red = new DTLibrary.DTColor.ctor(255, 0, 0);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(53, 483, 895, 162, red, false);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 650, "Line 1 Chess Comp Stomp with Hacks Chess\nLine 2\nLine 3\nLine 4", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.TestingFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.TitleScreenFrame(this.globalState, this.sessionState);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.One) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.One)) {
                    return new ChessCompStompWithHacksLibrary.TestingKeyboardFrame(this.globalState, this.sessionState);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Two) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Two)) {
                    return new ChessCompStompWithHacksLibrary.TestingMouseFrame(this.globalState, this.sessionState);
                }

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Three) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Three)) {
                    return new ChessCompStompWithHacksLibrary.TestingFontFrame(this.globalState, this.sessionState);
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(50, 650, "1) Test keyboard\n2) Test mouse\n3) Test font", ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt, DTLibrary.DTColor.Black());
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.TestingKeyboardFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null,
            x: 0,
            y: 0
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;

                this.x = 50;
                this.y = 50;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.TestingFrame(this.globalState, this.sessionState);
                }

                var delta = (Bridge.Int.div(this.globalState.ElapsedMicrosPerFrame, 2000)) | 0;

                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.LeftArrow)) {
                    this.x = (this.x - delta) | 0;
                }
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.RightArrow)) {
                    this.x = (this.x + delta) | 0;
                }
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.DownArrow)) {
                    this.y = (this.y - delta) | 0;
                }
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.UpArrow)) {
                    this.y = (this.y + delta) | 0;
                }

                if (this.x < 0) {
                    this.x = 0;
                }
                if (this.x > ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH) {
                    this.x = ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH;
                }
                if (this.y < 0) {
                    this.y = 0;
                }
                if (this.y > ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT) {
                    this.y = ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT;
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this.x - 5) | 0), ((this.y - 5) | 0), 11, 11, DTLibrary.DTColor.Black(), true);
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.TestingMouseFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null,
            x: 0,
            y: 0,
            color: 0,
            shouldFill: false
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;

                this.x = 0;
                this.y = 0;
                this.color = 0;
                this.shouldFill = true;
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.Esc)) {
                    return new ChessCompStompWithHacksLibrary.TestingFrame(this.globalState, this.sessionState);
                }

                this.x = mouseInput.DTLibrary$IMouse$GetX();
                this.y = mouseInput.DTLibrary$IMouse$GetY();

                if (mouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsLeftMouseButtonPressed()) {
                    this.color = (this.color + 1) | 0;
                    if (this.color === 4) {
                        this.color = 0;
                    }
                }

                if (mouseInput.DTLibrary$IMouse$IsRightMouseButtonPressed() && !previousMouseInput.DTLibrary$IMouse$IsRightMouseButtonPressed()) {
                    this.shouldFill = !this.shouldFill;
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                var dtColor;

                switch (this.color) {
                    case 0: 
                        dtColor = DTLibrary.DTColor.Black();
                        break;
                    case 1: 
                        dtColor = new DTLibrary.DTColor.ctor(255, 0, 0);
                        break;
                    case 2: 
                        dtColor = new DTLibrary.DTColor.ctor(0, 255, 0);
                        break;
                    case 3: 
                        dtColor = new DTLibrary.DTColor.ctor(0, 0, 255);
                        break;
                    default: 
                        throw new System.Exception();
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(((this.x - 5) | 0), ((this.y - 5) | 0), 11, 11, dtColor, this.shouldFill);
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });

    Bridge.define("ChessCompStompWithHacksLibrary.TitleScreenFrame", {
        inherits: [DTLibrary.IFrame$4(ChessCompStompWithHacksLibrary.ChessImage,ChessCompStompWithHacksLibrary.ChessFont,ChessCompStompWithHacksLibrary.ChessSound,ChessCompStompWithHacksLibrary.ChessMusic)],
        fields: {
            globalState: null,
            sessionState: null,
            volumePicker: null,
            startButton: null,
            continueButton: null,
            quitButton: null,
            clearDataButton: null,
            creditsButton: null
        },
        alias: [
            "ProcessExtraTime", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessExtraTime",
            "GetNextFrame", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$GetNextFrame",
            "ProcessMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$ProcessMusic",
            "Render", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$Render",
            "RenderMusic", "DTLibrary$IFrame$4$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$ChessCompStompWithHacksLibrary$ChessSound$ChessCompStompWithHacksLibrary$ChessMusic$RenderMusic"
        ],
        ctors: {
            ctor: function (globalState, sessionState) {
                this.$initialize();
                this.globalState = globalState;
                this.sessionState = sessionState;
                this.volumePicker = null;

                var buttonWidth = 150;

                this.startButton = new ChessCompStompWithHacksLibrary.Button(((Bridge.Int.div((((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH - buttonWidth) | 0)), 2)) | 0), 300, buttonWidth, 50, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Start", 35, 13, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.continueButton = new ChessCompStompWithHacksLibrary.Button(((Bridge.Int.div((((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH - buttonWidth) | 0)), 2)) | 0), 300, buttonWidth, 50, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Continue", 15, 13, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.quitButton = new ChessCompStompWithHacksLibrary.Button(((Bridge.Int.div((((ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH - buttonWidth) | 0)), 2)) | 0), 230, buttonWidth, 50, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Quit", 46, 13, ChessCompStompWithHacksLibrary.ChessFont.Fetamont20Pt);

                this.clearDataButton = new ChessCompStompWithHacksLibrary.Button(globalState.ShowSoundAndMusicVolumePicker ? 160 : 10, 10, 200, 31, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Reset data", 40, 6, ChessCompStompWithHacksLibrary.ChessFont.Fetamont16Pt);

                this.creditsButton = new ChessCompStompWithHacksLibrary.Button(895, 5, 100, 35, new DTLibrary.DTColor.ctor(200, 200, 200), new DTLibrary.DTColor.ctor(250, 249, 200), new DTLibrary.DTColor.ctor(252, 251, 154), "Credits", 13, 10, ChessCompStompWithHacksLibrary.ChessFont.Fetamont14Pt);
            }
        },
        methods: {
            ProcessExtraTime: function (milliseconds) { },
            GetNextFrame: function (keyboardInput, mouseInput, previousKeyboardInput, previousMouseInput, displayProcessing, soundOutput, musicProcessing) {
                if (this.volumePicker == null) {
                    this.volumePicker = new ChessCompStompWithHacksLibrary.SoundAndMusicVolumePicker(0, 0, soundOutput.DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$GetSoundVolume(), this.globalState.MusicVolume, this.globalState.ElapsedMicrosPerFrame);
                }

                if (this.globalState.ShowSoundAndMusicVolumePicker) {
                    this.volumePicker.ProcessFrame(mouseInput, previousMouseInput);
                    soundOutput.DTLibrary$ISoundOutput$1$ChessCompStompWithHacksLibrary$ChessSound$SetSoundVolume(this.volumePicker.GetCurrentSoundVolume());
                    this.globalState.MusicVolume = this.volumePicker.GetCurrentMusicVolume();
                }

                if (this.sessionState.HasStarted) {
                    var clickedContinueButton = this.continueButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedContinueButton) {
                        if (this.sessionState.GetGameLogic() == null) {
                            return new ChessCompStompWithHacksLibrary.HackSelectionScreenFrame(this.globalState, this.sessionState);
                        } else {
                            return new ChessCompStompWithHacksLibrary.ChessFrame(this.globalState, this.sessionState);
                        }
                    }
                } else {
                    var clickedStartButton = this.startButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedStartButton) {
                        return new ChessCompStompWithHacksLibrary.IntroScreenFrame(this.globalState, this.sessionState);
                    }
                }

                if (!this.globalState.IsWebBrowserVersion) {
                    var clickedQuitButton = this.quitButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedQuitButton) {
                        return null;
                    }
                }

                if (this.sessionState.HasStarted) {
                    var clickedClearDataButton = this.clearDataButton.ProcessFrame(mouseInput, previousMouseInput);
                    if (clickedClearDataButton) {
                        return new ChessCompStompWithHacksLibrary.ClearDataConfirmationFrame(this.globalState, this.sessionState, this);
                    }
                }

                var clickedCreditsButton = this.creditsButton.ProcessFrame(mouseInput, previousMouseInput);
                if (clickedCreditsButton) {
                    return new ChessCompStompWithHacksLibrary.CreditsFrame(this.globalState, this.sessionState);
                }

                if (this.globalState.DebugMode) {
                    if (keyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.T) && !previousKeyboardInput.DTLibrary$IKeyboard$IsPressed(DTLibrary.Key.T)) {
                        return new ChessCompStompWithHacksLibrary.TestingFrame(this.globalState, this.sessionState);
                    }
                }

                return this;
            },
            ProcessMusic: function () {
                this.globalState.ProcessMusic();
            },
            Render: function (displayOutput) {
                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawRectangle(0, 0, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_WIDTH, ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.WINDOW_HEIGHT, new DTLibrary.DTColor.ctor(223, 220, 217), true);

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(182, 510, "Chess Comp Stomp With Hacks", ChessCompStompWithHacksLibrary.ChessFont.Fetamont32Pt, DTLibrary.DTColor.Black());

                if (this.sessionState.HasStarted) {
                    this.clearDataButton.Render(displayOutput);
                }

                if (this.sessionState.HasStarted) {
                    this.continueButton.Render(displayOutput);
                } else {
                    this.startButton.Render(displayOutput);
                }

                if (!this.globalState.IsWebBrowserVersion) {
                    this.quitButton.Render(displayOutput);
                }

                displayOutput.DTLibrary$IDisplayOutput$2$ChessCompStompWithHacksLibrary$ChessImage$ChessCompStompWithHacksLibrary$ChessFont$DrawText(958, 55, "v1.00", ChessCompStompWithHacksLibrary.ChessFont.Fetamont12Pt, DTLibrary.DTColor.Black());

                this.creditsButton.Render(displayOutput);

                if (this.globalState.ShowSoundAndMusicVolumePicker) {
                    if (this.volumePicker != null) {
                        this.volumePicker.Render(displayOutput);
                    }
                }
            },
            RenderMusic: function (musicOutput) {
                this.globalState.RenderMusic(musicOutput);
            }
        }
    });
});
