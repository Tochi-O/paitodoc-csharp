-- SQLite
CREATE TABLE appointment (
    appId INTEGER PRIMARY KEY AUTOINCREMENT,
    docname VARCHAR (255) NOT NULL,
    docemail VARCHAR (255) NOT NULL,
    patientname VARCHAR (255) NOT NULL,
    patientemail VARCHAR (255) NOT NULL,
    date VARCHAR (255) NOT NULL,
    time VARCHAR (255) NOT NULL
);