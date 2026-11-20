# Verification and laptop checklist

Prepared on **20 November 2026**.

## Executed checks

| Check | Result |
| --- | --- |
| Core/data compilation | Passed, no warnings or errors |
| xUnit suite | **42 passed, 0 failed, 0 skipped** |
| MAUI Android Debug build | Passed, no warnings or errors; includes C# and compiled XAML bindings |
| Actual SQLite schema creation | Passed through EF migrations in the integration tests |
| Persistence across new service providers | Passed |
| Category relationship, CRUD, initialization concurrency | Passed |
| Empty title, ordering and exact 5 m/s boundary | Passed |
| HTTP JSON mapping, unexpected units and failed responses | Passed with controlled HTTP responses |
| Live Open-Meteo request through the app's C# weather service | Passed; temperature and wind deserialized successfully with the expected units |
| Fresh source copy with all bin/obj folders removed | Restored successfully; all 42 tests passed and MAUI Android rebuilt with 0 warnings/errors |

Build tools used: .NET SDK 9.0.321; MAUI Controls 9.0.120; EF Core SQLite 9.0.10; CommunityToolkit.Mvvm 8.4.0. Tests use xUnit 2.9.3.

The first test command hit this environment's restriction on parallel MSBuild worker pipes. The same test suite then ran successfully with a single MSBuild node and shared compilation disabled. This was an environment execution setting, not a change to the test cases:

```text
dotnet test tests/LifeManagementApp.Tests/LifeManagementApp.Tests.csproj --no-restore -m:1 -nr:false -p:UseSharedCompilation=false
```

The portable tests are network-independent. They exercise the same Core and Data assemblies that the MAUI app uses. Temporary test databases are separate from the user's app database.

## Platform limits

The preparation environment runs Linux. The Windows executable and native UI have **not** been run here. The Android project has been compiled, but an interactive device/emulator session has not been used. A successful build checks source/XAML correctness; it does not prove native layout, Shell navigation or every device interaction at runtime.

## Finish these checks on the presentation laptop

- [ ] Extract the ZIP fully, open the solution and select LifeManagementApp as the startup project.
- [ ] Run with Windows Machine and confirm that Today opens without a startup exception.
- [ ] Navigate between Today and My tasks using the tabs and View all.
- [ ] Open New task from both tabs; save valid tasks and confirm the editor returns to the correct tab.
- [ ] Confirm the title is required; spaces alone must not enable Save.
- [ ] Enter four or more tasks with priorities and dates; verify the Home top-three order.
- [ ] Change list sorting to Due date and back to Priority.
- [ ] Try category/status filters and title search, including no matches.
- [ ] Complete and reopen a task; verify Home counts/top-three refresh on returning.
- [ ] Edit an existing task; confirm the ID is retained and no duplicate appears.
- [ ] Cancel a delete, then confirm a delete on an expendable test task.
- [ ] Close and reopen the app; confirm titles, completion states and categories persist.
- [ ] Confirm live temperature/wind and the displayed m/s unit; press Refresh.
- [ ] Disconnect the network and refresh weather: a message should appear while tasks remain usable. Reconnect and refresh again.
- [ ] Test a narrow window and longer task titles for clipping and scrolling.
- [ ] Run the tests in Test Explorer on this laptop.
- [ ] Restore any practice edits, push the tested main commit and record its hash before submitting.

Use `scripts/Verify-Project.ps1` for the Windows build/test commands if desired. Runtime checks above still require opening the app.

## Review of course-page differences

The actual submission activity sets 22 November 2026 at 23:59; other text contains older/conflicting deadline, duration and repository-access information. The differences are documented in `COURSE_REQUIREMENTS.md`. The scheduler already showed a 2 December 11:06–11:16 booking; no external submission, publication or booking change was performed.
