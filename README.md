# Kurmann Messaging

`Kurmann.Messaging` ist ein leistungsfähiger, asynchroner Nachrichtendienst, konzipiert für .NET-Anwendungen, der Entwicklern das Publizieren und Abonnieren von Nachrichten mit minimaler Kopplung zwischen Komponenten ermöglicht.

## Funktionalitäten

- **Asynchrones Messaging**: Ermöglicht das Senden und Empfangen von Nachrichten auf asynchrone Weise.
- **Typsichere Nachrichten**: Definiert und verarbeitet Nachrichten auf Basis ihres Typs.
- **Erweiterbar**: Einfach in bestehende .NET-Projekte zu integrieren und anzupassen.
- **Thread-sicheres Subskribieren und Unsubskribieren**: Gewährleistet die Integrität von Nachrichtenlisten in multithreaded Szenarien.

## Schnellstart

### Installation

Das NuGet-Paket kann mit folgendem Befehl in Ihr .NET-Projekt installiert werden:

```bash
dotnet add package Kurmann.Messaging
```

### Grundlegende Verwendung

Hier ist ein einfaches Beispiel, wie Sie den Messaging-Dienst in Ihre Anwendung integrieren können:

```csharp
// Nachricht definieren
public class MyMessage : EventMessageBase
{
    public string Content { get; set; }
}

// Nachrichten senden
await messageService.Publish(new MyMessage { Content = "Hello World" });

// Nachrichten empfangen
messageService.Subscribe<MyMessage>(async (msg) =>
{
    Console.WriteLine(msg.Content);
});
```

### Erweiterte Konfiguration

Bitte schauen Sie sich die `MessageService`-Klasse für weitere Konfigurationsmöglichkeiten und erweiterte Nutzung an.

## Mitwirken

Wir freuen uns über Beiträge in Form von Pull Requests, Bug Reports oder Feature Requests. Bitte lesen Sie hierzu unsere [CONTRIBUTING.md](CONTRIBUTING.md).

## Lizenz

Dieses Projekt ist unter der Apache 2.0 Lizenz lizenziert - siehe die [LICENSE](LICENSE) Datei für Details.

## Kontakt

Falls Sie Fragen haben oder Unterstützung benötigen, erstellen Sie bitte ein Issue im GitHub-Repository.

## Änderungsverlauf

Dieses Projekt hält sich an die Semantische Versionierung (SemVer).

## Unveröffentlicht

- keine

## 0.3.0 - 2024-04-07

### Geändert

- Weiterentwicklungen des Legacy-Messagingdienstes
- Vereinfachter Workflow für Zwischenreleases mit automatischer Datumsvergabe bei Vorschauversionen

## 0.2.0 - 2024-04-06

### Hinzugefügt

- Integration bestehender Messaging-Dienst aus dem eigenen Projekt "Infuse Media Integrator."
- Dieses ChangeLog in die Readme-Datei eingebettet damit, aufgrund Restriktionen von NuGet, dieses Changelog direkt in das NuGet-Packet eingebettet wird.

## 0.1.0 - 2024-04-06

### Hinzugefügt

- Dieses Changelog erstellt
- Klassenbibliothek aus dem eigenen [.NET-Template](https://github.com/kurmann/Templates)
