# MicroCoG
 Social inference system for City of Gangsters save files, implemented in TELL (see the TELL repo).
 
To try it out, download the binary from the release, unzip and run the EXE file. This will take a minute to start because it's loading a 40MB save file from the game. After that, you can type live queries against the game state.

You can press Show Predicates to get a list of predicates, Help to get a list of possible queries, or you can select an Agent, Action, and Target, and it will show you the reactions of other character to the selected agent performing that action against that target. Violence and Killing are the most interesting actions.

WARNING: every time you type a key in the query box, it returns the query. If you run a long query, typing will get slow. Unfortunately, there's no way to kill a thread in the current version of .NET, so we can't just run it in the background.
