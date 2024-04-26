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

## Lizenz

Dieses Projekt ist unter der Apache 2.0 Lizenz lizenziert - siehe die [LICENSE](LICENSE) Datei für Details.

## Kontakt

Falls Sie Fragen haben oder Unterstützung benötigen, erstellen Sie bitte ein Issue im GitHub-Repository.
