﻿# Zeus

Zeus ist eine Implementierung des Spiels Schere, Stein, Papier mit Multiplayer-Funktionalität.

## Technik

Die Anwendung ist eine C# Windows Forms Anwendung.
Als Entwicklungsumgebung wurde Visual Studio benutzt.

## Features

* Verschlüsselter Datenverkehr
* IPv6-Unterstützung
* Integrierter Chat

## Spielanleitung

Der erste Spieler muss ein Spiel hosten.
Dazu wählt er in den Einstellungen, welche beim Starten des Spiels erscheinen, seinen Spielernamen, ein Kennwort für die Verschlüsselung des Datenverkehrs und den Port aus.
Anschließend gibt er noch die Rundenanzahl an und klickt auf *Server starten*.

Der zweite Spieler gibt ebenfalls seinen Spielernamen, das gleiche Kennwort sowie den gleichen Port an.
Danach gibt er die IP-Adresse oder den Hostnamen des anderen Spielers an und klickt auf *verbinden*.

Das Spiel wird nun bei beiden Spielern gestartet.
Die beiden Spieler können während und nach dem Spiel chatten.
