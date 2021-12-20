# Overview

In this game, you play against the AI in a chess match. The AI has vast computational resources, but you have hacks. Use your hacks to win!

# How to execute the program

Open the `Chess Comp Stomp with Hacks.html` file in a web browser.

# Licensing

##### Programming

The source code of Chess Comp Stomp with Hacks is licensed under the MIT license. However, note that it uses dependencies and other assets that are licensed under different licenses.

This project uses Bridge.NET to compile the C# code into javascript. Bridge.NET is licensed under the Apache License Version 2.0. See https://github.com/dtsudo/Bridge.NET and https://github.com/dtsudo/Bridge.NET-CLI for more details about Bridge.NET.

##### Font

The font used by this game was generated by metaflop. (See http://www.metaflop.com/modulator for more details about metaflop.) As the website notes: "All outline-based fonts (webfonts or otf) that are generated with this project are licensed under the SIL Open Font License v1.1 (OFL). This means that you can freely use and extend the fonts and also use them commercially. Any derivative work has to be made freely available under the same license."

##### Images

The game uses images from Kenney Asset Pack. These images are licensed under Creative Commons Zero (CC0). See www.kenney.nl for more details.

The images of chess pieces were created by Cburnett (https://en.wikipedia.org/wiki/User:Cburnett) and are licensed under the BSD license.

# How to compile the source code

This project uses Bridge.NET to compile the C# code into javascript. The Bridge CLI (https://github.com/dtsudo/Bridge.NET-CLI) needs to be installed so that we can run the Bridge compiler on the command line.

Once the Bridge CLI is installed, go to the source code folder and run `bridge build` to compile the C# code:

* `cd "Source code/ChessCompStompWithHacks/"`
* `bridge build`

This will generate a few files in the `Source code/ChessCompStompWithHacks/dist/` folder.

We also need the font and image files:

* Copy the `Data` folder to `Source code/ChessCompStompWithHacks/dist/`

Then, to run the program, simply run `Source code/ChessCompStompWithHacks/dist/Chess Comp Stomp with Hacks.html` in a web browser.
