CREATE TABLE "User" (
	"username"	TEXT NOT NULL UNIQUE,
	"password"	TEXT NOT NULL,
	"token"	TEXT,
	PRIMARY KEY("username")
);;

CREATE TABLE "StoredFile" (
	"username"	TEXT NOT NULL,
	"filename"	TEXT NOT NULL,
	"size"	INTEGER NOT NULL,
	"hash"	TEXT NOT NULL,
	"data"	BLOB NOT NULL,
	FOREIGN KEY("username") REFERENCES "User"("username") ON DELETE CASCADE,
	PRIMARY KEY("username","filename")
);