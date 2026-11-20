# Presentation එකට ලෑස්ති වෙමු

## Booking එක සහ කාලය

20 November 2026 දා portal එකේ පෙන්වූ booking එක: **2 December 2026, 11:06–11:16, A3009**. Own laptop එකේ app එක ready කරගෙන යන්න.

Booking එකේ කාලය minutes 10. Overview එකත් minutes 10 කියනවා; අනෙක් text එකක 15 කියලා තියෙන නිසා මුලින් 10-minute version එක practice කරලා, වැඩිපුර ප්‍රශ්නවලටත් ලෑස්ති වෙන්න. Code සහ README open තබාගන්න පුළුවන්. Presentation එකේ AI භාවිතා කරන්න බැහැ.

## 1. Demo එකට කලින් tasks සකස් කරන්න

App එකේ මේ tasks ටික ඔයාම add කරන්න. Due dates demo දවසට ගැළපෙන්න දාන්න.

| Task | Priority | Due date | Status |
| --- | --- | --- | --- |
| Prepare project presentation | 5 | Tomorrow | Open |
| Review C# notes | 5 | Today | Open |
| Buy groceries | 4 | Today | Open |
| Tidy desk | 2 | Today | Open |
| Finished practice task | 5 | Yesterday | Done |

Home top 3 එක **Review C# notes → Prepare project presentation → Buy groceries** වෙන්න ඕන. Finished task එක top 3 එකේ නැති බව පෙන්නන්න.

Weather එක live නිසා demo දවසේ wind එක 5ට අඩු වෙන්න පුළුවන්. ඒක error එකක් නෙවෙයි. Alert boundary එක WeatherTests වලින් පෙන්නන්න. Live reading එක වෙනුවට fake number එකක් real weather වගේ පෙන්වන්න එපා.

## 2. Minutes 2ක demo script එක

**0:00–0:25:** Today page එක පෙන්වන්න. English වලින් මෙහෙම කියන්න:

“This app combines my daily tasks with the current weather in Vaasa. The home page shows the three highest-priority unfinished tasks. Equal priorities are ordered by due date.”

**0:25–0:45:** Weather සහ threshold එක පෙන්වන්න:

“The weather comes from Open-Meteo. Wind is requested in metres per second. The kite message appears only when wind is greater than five.”

**0:45–1:20:** My tasks tab එකට යන්න. Priority සහ Due date sorts මාරු කරන්න. Category එකක් තෝරන්න. Task එකක් Done කරලා Home top 3 එක update වෙන බව පෙන්වන්න.

**1:20–2:00:** New task එකක් add කරන්න. Empty title එකේ Save disabled බවත්, title දුන්නාම enabled බවත් පෙන්වන්න. එක task එකක් Edit කරලා data update වෙන බව පෙන්වන්න. Restart persistence check එක presentation එකට කලින් කරලා තියෙන්න ඕන.

## 3. අහන්න පුළුවන් ප්‍රශ්න සහ තේරුම් සහිත උත්තර

### Q1. Where is the top 3 selected?

File: `Core/Rules/TodoRules.cs`, method: **GetTopThree**.

සිංහල අදහස: completed tasks filter කරනවා. Priority descending, due date ascending, ඊට පස්සේ මුල් තුන ගන්නවා.

English: “GetTopThree filters out completed tasks, sorts by descending priority and then ascending due date, and takes at most three.”

### Q2. Where do you reject an empty title?

File: `Core/ViewModels/AddTodoViewModel.cs`, method: **CanSave**.

English: “CanSave uses IsNullOrWhiteSpace and the length limit. The title notifies SaveCommand when it changes, so the button rechecks its enabled state. The data service also validates a write.”

### Q3. What does ObservableProperty generate?

File: `AddTodoViewModel.cs`, field: **title**.

English: “It generates a public Title property, with change notifications. That updates XAML bindings. The class is partial because the generator contributes another part of the same class.”

### Q4. Why ObservableCollection instead of List?

File: `TodoListViewModel.cs`, property: **Todos**.

English: “ObservableCollection notifies the UI when items are added or removed. An item's own property changes need separate notifications, so TodoItem also inherits ObservableObject.”

### Q5. Explain the save path.

Open files in this order: **AddTodoViewModel → TodoRepository → TodoMapper → TodoDataService → AppDbContext**.

English: “The ViewModel validates form values and asks the repository to save. The adapter maps the screen model into a database entity. The data service creates a context and calls SaveChangesAsync. The generated ID is copied back before navigation.”

### Q6. What does DbContext do? Why a factory?

English: “DbContext represents a unit of database work and tracks entity changes. It is not thread-safe. The factory gives each operation its own short-lived context, even though the repository is a singleton.”

### Q7. What is the relationship in your database?

File: `Data/AppDbContext.cs`, **HasOne / WithMany / HasForeignKey**.

English: “One category can contain many tasks. TodoEntity.CategoryId is the foreign key. Include loads the category name with a task, and the task-list filter uses the category ID.”

### Q8. Why use migrations?

English: “Migrations record schema changes in version control. The app applies missing migrations when the local database is first used. A clean installation gets the same tables and categories. I do not mix EnsureCreated with migrations.”

### Q9. How does JSON become an object?

File: `Core/Services/OpenMeteoWeatherService.cs`; JSON model files in the same folder.

English: “GetFromJsonAsync deserializes into typed response classes. JsonPropertyName connects Open-Meteo field names with C# properties. Missing readings and unexpected units are rejected.”

### Q10. What happens without internet?

File: `Core/ViewModels/HomeViewModel.cs`, **RefreshWeatherAsync**.

English: “Weather loading catches the failure, clears the current weather and shows a useful message. The task-loading path is independent, so local tasks still work. Finally resets the loading state.”

### Q11. Why async/await?

English: “It lets the UI remain responsive while a network or database operation is waiting. It does not make the request itself faster. I avoid Result and Wait, which would block the UI thread.”

### Q12. Where are your dependencies created?

File: `MauiProgram.cs`.

English: “MauiProgram registers the service implementations. Constructor injection supplies them to ViewModels and pages. Shared stateless services are singleton; editor pages and ViewModels are transient so form state is fresh.”

### Q13. How does the edit page know which task to load?

Files: `MauiNavigationService.cs`, `AddTodoPage.xaml.cs`.

English: “The Edit command passes the selected ID to the navigation interface. Shell carries TodoId to IQueryAttributable. The page passes the ID to the ViewModel, which reloads the task from the repository.”

### Q14. Where is your converter?

File: `Converters/PriorityToColorConverter.cs`.

English: “It maps a priority number to a UI colour. That keeps colour choices out of the Model and ViewModel. The row binding uses the converter.”

### Q15. Did you use AI?

README එකේ තියෙන disclosure එකට ගැළපෙන ඇත්ත උත්තරයක් දෙන්න.

English: “I used ChatGPT to help generate and explain the initial code and tests. The README records that use. I have reviewed and practised the parts I am presenting.”

අවසාන sentence එක කියන්න කලින් ඒ review/practice එක ඔයා ඇත්තටම කරලා තියෙන්න ඕන.

## 4. Live change එකට practice

### Practice A: existing field එකක් list එකේ පෙන්වන්න

`Views/TodoListPage.xaml` row template එකේ title එකට යටින් මෙය දාන්න:

```xml
<Label Text="{Binding Notes}" FontSize="12" />
```

Run කරලා notes සහිත task එකක් පෙන්වන්න. ඒකට database change එකක් ඕන නැත්තේ Notes දැනටමත් model එකේත් entity එකේත් තියෙන නිසා.

### Practice B: priority display එක වෙනස් කරන්න

Task row එකේ `P5` display එක `Priority 5` වෙන්න StringFormat එක වෙනස් කරන්න. Sorting logic එක වෙනස් නොවන බව explain කරන්න.

### Practice C: අලුත් field එකක් සම්පූර්ණයෙන් add කරන්න

Practice copy එකක `EstimatedMinutes` කියන int field එක add කරලා බලන්න:

1. TodoEntity එකට property එක දාන්න.
2. TodoItem එකට property එක දාන්න.
3. TodoMapper method දෙකේම value copy කරන්න.
4. අවශ්‍ය නම් AddTodoViewModel field එක සහ form binding එක දාන්න.
5. EF migration එකක් generate කරලා file එක කියවන්න.
6. App එක run කරලා task එකක් save කරන්න. නැවත open කළාම value එක තියෙනවද බලන්න.

Presentation එකේ teacher තෝරන change එක වෙනස් විය හැකියි. මේකෙන් “field එකක් කොහෙ කොහෙද යන්නේ?” කියන අදහස ඉගෙනගන්න.

## 5. Laptop එකේ කලින් සූදානම් කරන්න

- NuGet restore සහ build අවසන් කරලා app එක run කරලා බලන්න.
- Charger එක ගන්න. Demo tasks කලින් සකස් කරන්න.
- README, TodoRules, HomeViewModel, AddTodoViewModel, MauiProgram, AppDbContext සහ weather service tabs open කරගන්න.
- Tests run කරලා result බලන්න.
- Internet නැති අවස්ථාවත් personally try කරලා බලන්න.
- Practice වෙනස්කම් පසු required wind limit එක 5ට සහ final code එක submitted commit එකට ආපසු ගන්න.
- ඔයා submit කළ commit hash එක සටහන් කරගන්න. Presentation එක එතැනින් පටන් ගන්න.

Code එක කටපාඩම් කියන එකට වඩා, data එක ගමන් කරන පාරත් එක් එක් class එකට ඒ වගකීම දුන්නේ ඇයි කියන එකත් explain කරන්න පුළුවන් වීම වැදගත්.
