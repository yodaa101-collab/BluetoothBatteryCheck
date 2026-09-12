\# Changelog



All notable changes to BatteryCheck will be documented here.



\## \[0.9.0] - 2026-09-12



\### Added



\- Compact desktop-widget style main window.

\- Detection of paired Bluetooth LE and classic Bluetooth devices.

\- Standard BLE battery percentage reading for connected devices.

\- Nearby-device scanning and Windows pairing workflow via \*\*+ Add Device\*\*.

\- \*\*Remove Device\*\* function for unpairing a selected Bluetooth device.

\- Configurable automatic battery refresh interval.

\- Optional \*\*Start BatteryCheck with Windows\*\* setting.

\- Optional window size and position persistence.

\- Local JSON settings persistence.

\- \*\*Create Desktop Shortcut\*\* option in Preferences.

\- Custom BatteryCheck application icon.

\- First-run disclaimer requiring acceptance before entering the application.

\- File, Settings, and Help menus.

\- About dialog.

\- Exit button in the main window.

\- Application attribution: \*\*Created by VA with OpenAI assistance\*\*.

\- Self-contained Windows x64 publishing support.

\- Open-source project documentation and MIT license.



\### Changed



\- Battery refresh uses cached standard BLE data and only queries battery information for devices Windows reports as connected.

\- Bluetooth access was made deliberately conservative to reduce the possibility of interfering with active Bluetooth peripherals.

\- Main window and footer were refined for a compact desktop utility layout.

\- Published Windows x64 release is self-contained and does not require users to install the .NET 9 runtime separately.



\### Removed



\- Active GATT Explorer/developer-mode functionality from the normal application.

\- Experimental GATT Explorer files from the release source tree.



\### Known Limitations



\- Battery percentages may show \*\*Unavailable\*\* for devices that do not expose the standard BLE Battery Service to Windows.

\- Some Bluetooth devices use manufacturer-specific battery-reporting methods that BatteryCheck does not currently support.

\- Bluetooth pairing may require normal Windows confirmation or authentication.

\- A desktop shortcut points to the location of `BatteryCheck.exe` when the shortcut is created. Moving the EXE afterward can break that shortcut.



\## Release Status



BatteryCheck v0.9.0 is a pre-1.0 release intended for real-world testing and feedback before a future v1.0 release.

