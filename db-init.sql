CREATE TABLE "User" (
	"username"	TEXT NOT NULL UNIQUE,
	"passhash"	TEXT NOT NULL,
	PRIMARY KEY("username")
);

CREATE TABLE "StoredFile" (
	"username"	TEXT NOT NULL,
	"name"	TEXT NOT NULL,
	"size"	INTEGER NOT NULL,
	"hash"	TEXT NOT NULL,
	"data"	BLOB NOT NULL,
	PRIMARY KEY("username","name"),
	FOREIGN KEY("username") REFERENCES "User"("username") ON DELETE CASCADE
);