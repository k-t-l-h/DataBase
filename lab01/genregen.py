start = ["art punk",
"alternative rock",
"college rock",
"crossover thrash",
"crust punk",
"experimental rock",
"folk punk",
"gothic rock",
"grunge",
"hardcore punk",
"hard rock",
"indie rock",
"lo-fi ",
"new wave",
"progressive rock",
"punk",
"shoegaze",
"steampunk",
"2-step",
"8bit",
"ambient",
"bassline",
"chillwave",
"chiptune",
"crunk",
"downtempo",
"drum & bass",
"electro",
"electro-swing",
"electronica",
"electronic rock",
"hardstyle",
"idm/experimental",
"industrial",
"trip hop"]


f = open("genres.txt", "w")

for i in range(len(start)):
    st = "{0}, {1};".format(i+1, start[i])
    f.write(st)
f.close()
