# BatteryCheck local test plan

Perform this checklist on the Windows 11 laptop before publishing the project.

## 1. Build

- Open `BatteryCheck.slnx` in Visual Studio.
- Select Release configuration.
- Build Solution.
- Confirm 0 errors. Resolve warnings that indicate functional problems before release.

## 2. Existing devices

- Start BatteryCheck with Bluetooth enabled.
- Confirm paired Bluetooth devices appear.
- Confirm connected devices say `Connected` and known-but-disconnected devices say `Paired`.
- Confirm no duplicate rows for the same physical device where possible.

## 3. Battery readings

Test at least one device known to expose the standard BLE Battery Service.

- Confirm the percentage is plausible.
- Click Refresh several times.
- Confirm mouse, keyboard, headset, and other active Bluetooth peripherals remain responsive.
- Confirm unsupported devices show `Unavailable` rather than an invented value.

## 4. Add device

- Put an unpaired Bluetooth device into discoverable/pairing mode.
- Click `+ Add Device`.
- Confirm it appears in the scan list.
- Select it and click `Add / Pair`.
- Complete any Windows pairing prompt.
- Confirm the dialog closes after successful pairing and the main list refreshes.

## 5. Preferences

- Change refresh interval from 60 seconds to another value.
- Close and reopen Preferences and confirm the value persists after OK.
- Restart BatteryCheck and confirm the setting persists.
- Enable and disable `Start BatteryCheck with Windows` and verify the Windows startup behavior.
- Verify Cancel does not save changes.

## 6. Widget/window behavior

- Move the window and resize it.
- Close and reopen BatteryCheck; confirm size and position restore when enabled.
- Disable those settings and verify the corresponding behavior is no longer persisted.
- Confirm normal Close exits the application; there is no hidden tray instance.

## 7. Edge cases

- Start BatteryCheck with Bluetooth turned off.
- Turn Bluetooth back on and refresh.
- Test with no paired devices.
- Test a device going offline between refreshes.
- Test multiple monitors, including reopening after one monitor is disconnected.
