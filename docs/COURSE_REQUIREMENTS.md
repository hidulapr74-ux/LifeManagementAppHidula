# Course requirements collected on 20 November 2026

Course: **TT00BI73-3008 Client-Side Programming (IT2024, IT2024-A)**. Project: **LifeManagementApp**. Implementation 3008 requires individual work.

## Core behaviour

| Area | Required behaviour |
| --- | --- |
| Home | At most three unfinished tasks, sorted by descending priority and then ascending due date |
| Weather | Display at least temperature and current wind speed |
| Kite notice | A clear alert only when wind exceeds 5 m/s; exactly 5 is below the alert condition |
| Task list | All tasks in a CollectionView; priority and due-date sort orders |
| Add task | A page or form for new tasks; reject empty titles |
| Task fields | Id, Title, Priority, DueDate and IsDone; other fields may be added |
| Course stack | .NET 9 / MAUI 9, C#, XAML, MVVM, CommunityToolkit.Mvvm, services and DI |
| Higher storage levels | SQLite, EF Core Code First, add/query/update; a relationship and async queries at the highest level |
| Code conventions | One class per file; separate Models, ViewModels, Views, Services and data access |

Open-Meteo needs no API key. The course explicitly calls out its default wind unit: request `wind_speed_unit=ms` to compare readings with a metres-per-second threshold. This project fixes the location to the assignment's Vaasa example (63.10, 21.62).

## Work points: 50 maximum

| Criterion | Max | What reaches the high level |
| --- | ---: | --- |
| 1. Functionality | 10 | Home top 3 (2), weather (2), kite notice (1), list and both sorts (3), adding and validation (2) |
| 2. Pages/navigation | 5 | Multiple Shell pages and parameter passing, such as the selected task ID for editing |
| 3. Storage | 8 | SQLite + EF Core CRUD, a relationship and asynchronous queries |
| 4. MVVM architecture | 9 | Model/ViewModel/service separation, no controls in VMs, interfaces, clear folders and shared logic |
| 5. Binding/Toolkit | 5 | ObservableProperty, RelayCommand, ObservableCollection, x:DataType and a converter |
| 6. DI | 5 | Registered services/interfaces, constructor injection, pages/VMs created by DI and explained lifetimes |
| 7. REST | 5 | Async API call, typed JSON, correct units, loading state, useful failure handling and responsive UI |
| 8. Git/documentation | 3 | Code in Git, descriptive incremental commits and a README with description, run instructions, class diagram and AI disclosure |

The old separate 5-point presentation criterion was removed. Its points were redistributed; do not add its crossed-out row to the total.

| Bonus | Max | Evidence |
| --- | ---: | --- |
| B1 | 2 | Hosted issues defining features, feature branches and merges/PRs referring to those actual issues |
| B2 | 2 | Working extra features; one point per accepted feature, up to two |
| B3 | 1 | xUnit tests for a service or ViewModel, such as task ordering or the kite threshold |

Listed extra-feature options include completing/editing/deleting, settings, forecasts, weather caching, geolocation, dark theme and categories/filtering. This implementation includes completing/editing/deleting and category filtering. Acceptance and points remain the teacher's decision.

## Understanding and passing

The teacher first assigns work points from the repository. During the presentation each relevant criterion receives an understanding factor: **0, 0.25, 0.5, 0.75 or 1**. Explaining the behaviour, the reason and a small modification earns stronger factors. Project points are the rounded sum of criterion work points multiplied by their factors.

Criteria 1–7 and bonuses B2/B3 receive factors. Criterion 8 and B1 do not.

A passed project requires an app that starts, working Home/task pages and at least **20 work points before factors**. The presentation is mandatory and its understanding assessment cannot be retaken. To pass the whole course: at least 20 exam points, a passed project and at least 40 total course points. The exam contributes 50 points and the project contributes 50 plus up to 5 bonus points.

| Course points | Grade |
| --- | --- |
| 0–39 | Fail |
| 40–51 | 1 |
| 52–63 | 2 |
| 64–75 | 3 |
| 76–87 | 4 |
| 88 or more | 5 |

## Submission and scheduling

- The actual submission activity and project overview both show **Sunday 22 November 2026, 23:59**.
- Presentation booking closes at the same deadline.
- Push the final work to `main` and submit the Git link and exact short commit hash.
- Include target grade, tested platform, storage type, bonus features and known issues.
- Bring the laptop with the app ready to run to **A3009**.
- Session dates shown: 25 November (10:00–11:30), 26 November (12:30–14:00) and 2 December (10:00–11:30).
- The existing booking observed for this account was **2 December, 11:06–11:16**. No booking was created or changed during preparation.

Times above are the displayed portal times. Confirm the current activity if the teacher later updates it.

## Differences between the course pages

| Topic | What conflicts | Practical interpretation used here |
| --- | --- | --- |
| Deadline | Main assignment text still says 10 November; actual submission activity and overview say 22 November | Use the actual activity's 22 November deadline |
| Presentation length | Assignment and scheduler introduction say 15 minutes; overview says 10 minutes; the actual booking spans 10 minutes | Prepare a complete 10-minute version plus extra detail if requested |
| GitHub visibility | Main assignment/tools page allows private GitHub with `tuo-vamk`; actual submission page and overview require public GitHub | VAMK Git private with `tuo` is consistent across the pages; otherwise follow the submission activity's public-repository instruction or obtain clarification |
| Assessed version | Overview mentions deadline state; actual submission page specifies submitted commit/time | Submit only a finished, tested commit and record its exact hash; do not rely on later commits being assessed |
| Late work | Overview gives a 10-point cap for lateness/missing the booked presentation; submission activity says late submissions need an agreed extension | Submit on time; if something prevents it, resolve the extension with the teacher |

The overview says late submission or missing a booked presentation caps points at 10 with no bonus; ordinary pass requirements and the mandatory presentation remain. It lists documented illness as the accepted reason for rescheduling with ordinary scoring. The project preparation does not send messages or request extensions.

## Submission field template

```text
Git link:
Last commit:
Target grade:
Tested on:
Persistence: SQLite + EF Core
Bonuses: B1 issues + feature branches: [state actual evidence]
         B2 extra features: complete/edit/delete; category filtering
         B3 tests: yes
Known issues: [state actual remaining issues]
```

The README must include a short, honest **AI use** section. AI assistance is allowed; AI is prohibited during the presentation. The project page refers to a P7 README template and specifically names description, how to run, class diagram and AI disclosure. Those sections are included here; this document does not invent a separately retrieved P7 file.

## Taught material used to choose the implementation

| Course topic | Application example |
| --- | --- |
| Classes, properties and interfaces | TodoItem, plain database entities and ITodoRepository |
| Inheritance and events | ViewModels inherit ObservableObject through BaseViewModel; property-change notifications update bindings |
| Generics/collections/lambdas | ObservableCollection of tasks and LINQ task predicates |
| LINQ, exceptions, async | Priority/date ordering; caught errors; awaited database and HTTP calls |
| MAUI/XAML and layouts | Grid, Border, labels, entries, pickers, DatePicker and CollectionView |
| Toolkit MVVM | Field-style ObservableProperty, generated commands, CanExecute and dependent-property notifications |
| Shell and shared data | Today/tasks tabs and a task ID passed to the editor |
| EF Core/migrations | Separate data library, factory-created contexts, entity mapping and migration-driven schema creation |
| DI | Shared services; transient pages/VMs; short-lived contexts |
| REST and JSON | HttpClient with typed Open-Meteo JSON mappings and explicit units |
| Git and xUnit | Development branches/commits and portable tests |

Read sources: course section overviews; lecture notes 1, 2, 6 and 7; Toolkit essentials and field-syntax guide; task 7's MVVM instructions; database/migrations guide; installing-tools page; Git guide; project specification, submission activity and scheduler.

## Source links

- [Course](https://portal.vamk.fi/course/view.php?id=20189)
- [Project overview](https://portal.vamk.fi/course/view.php?id=20189&section=6#tabs-tree-start)
- [Full brief](https://portal.vamk.fi/mod/page/view.php?id=1066900)
- [Submission](https://portal.vamk.fi/mod/assign/view.php?id=1060859)
- [Scheduler](https://portal.vamk.fi/mod/scheduler/view.php?id=1066904)
- [Toolkit](https://portal.vamk.fi/mod/page/view.php?id=1061446)
- [Field syntax](https://portal.vamk.fi/mod/page/view.php?id=1061646)
- [Database guide](https://portal.vamk.fi/mod/page/view.php?id=1067628)
- [Git guide](https://portal.vamk.fi/mod/page/view.php?id=1049254)
- [Tools](https://portal.vamk.fi/mod/page/view.php?id=1049251)
