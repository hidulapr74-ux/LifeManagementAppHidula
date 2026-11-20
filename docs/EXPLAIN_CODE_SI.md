# Code එක තේරුම් ගනිමු

## 1. මුලින් app එකේ වැඩේ තේරුම් ගන්න

මේ app එකේ වැඩ දෙකක් තියෙනවා: ඔයාගේ tasks local database එකේ තියාගන්න එක සහ Vaasa weather එක internet එකෙන් ගන්න එක. Today page එකෙන් ඉදිරියට කරන්න වැදගත් tasks තුනත් weather එකත් පෙන්වනවා.

Solution එකේ projects හතරක් තිබුණත් ඒවා එක app එකේ කොටස්:

| Project | සරල තේරුම |
| --- | --- |
| LifeManagementApp | ඔයාට පෙනෙන screens සහ Windows/Android සම්බන්ධ code |
| LifeManagementApp.Core | Screens වලට අවශ්‍ය data සහ වැඩ කරන logic |
| LifeManagementApp.Data | SQLite database එකත් එක්ක කරන වැඩ |
| LifeManagementApp.Tests | App එකේ නීති හරිද කියලා පරීක්ෂා කරන code |

Core වෙනම තැබුවේ ViewModel tests run කරන්න MAUI window එකක් අවශ්‍ය නොවෙන්න. Data project එක වෙනම තියෙන්නේ teacherගේ database guide එකේ උගන්වපු separation එකට ගැළපෙන්න.

## 2. Model කියන්නේ මොකක්ද?

`Core/Models/TodoItem.cs` open කරන්න. එක TodoItem object එකක් කියන්නේ එක task එකක්.

| Property | උදාහරණයක් / තේරුම |
| --- | --- |
| Id | Database එක දෙන වෙනස් අංකය, උදා. 7 |
| Title | Read C# notes |
| Priority | 1–5 අතර අගයක්; 5 වැඩිම වැදගත්කම |
| DueDate | Task එක කරන්න තියෙන දිනය |
| IsDone | ඉවර නම් true; නැත්නම් false |
| Notes | අමතර විස්තර |
| CategoryId | අදාළ category එකේ අංකය |
| CategoryName | Screen එකේ පෙන්වන Study වගේ නම |

`TodoItem` කියන්නේ screen model එක. `Data/Entities/TodoEntity.cs` කියන්නේ database row එක. ඒවා වෙනම තැබුවාම database code එකට UI එක ගැන දැනගන්න අවශ්‍ය නැහැ. `TodoMapper` දෙක අතර fields copy කරනවා.

`CategoryEntity` එකකට TodoEntity කිහිපයක් අයිති වෙන්න පුළුවන්. ඒක **one-to-many relationship** එක. TodoEntity එකේ CategoryId කියන්නේ foreign key එක. මේ app එකේ Study, Work, Home, Personal categories තියෙනවා.

## 3. MVVM කියන්නේ කොටස් තුනක්

**View:** `Views/HomePage.xaml` වගේ screen එක. Labels, buttons, layouts මෙතන.

**ViewModel:** `Core/ViewModels/HomeViewModel.cs` වගේ class එක. පෙන්වන්න ඕන data සහ button එක ඔබද්දී කරන්න ඕන දේ තීරණය කරනවා.

**Model:** TodoItem, WeatherInfo වගේ data objects.

ViewModel එකේ `TitleEntry.Text` හෝ `Label.TextColor` වගේ UI control references නැහැ. ඒ නිසා UI design එක වෙනස් කළත් database logic එක වෙනස් කරන්න අවශ්‍ය නැහැ. ඒ ViewModel එක test එකකින් window එකක් නැතුව පරීක්ෂා කරන්නත් පුළුවන්.

## 4. Binding එකෙන් UI එකට data යන්නේ කොහොමද?

`AddTodoPage.xaml` එකේ මේ වගේ binding එකක් තියෙනවා:

```xml
<Entry Text="{Binding Title}" />
```

Page එකේ BindingContext එක AddTodoViewModel object එක. Entry එකේ Text සහ ViewModel එකේ Title property එක සම්බන්ධ වෙනවා. User type කළාම Title update වෙනවා. ViewModel එක Title වෙනස් කළාම screen එක update වෙනවා.

`x:DataType` කියන්නේ binding එක බලාපොරොත්තු වෙන class එක compiler එකට කියන එක. Page එකට ViewModel type එක; task row එකට TodoItem type එක. Property නම වැරදුණොත් build කරනකොටම අල්ලගන්න උදව් වෙනවා.

## 5. Toolkit එක කරන්නේ මොකක්ද?

`AddTodoViewModel.cs` එකේ මේ code එක බලන්න:

```csharp
[ObservableProperty]
[NotifyCanExecuteChangedFor(nameof(SaveCommand))]
[NotifyPropertyChangedFor(nameof(TitleCounter))]
private string title = "";
```

මෙතන private field එක `title`. Toolkit source generator එක public `Title` property එක හදනවා. ඒ property එක වෙනස් වුණාම PropertyChanged event එක යන නිසා binding එක screen එක update කරනවා.

TitleCounter එකත් update කරන්න කියලා දෙවැනි notification එක දෙනවා. Save button එක enable ද කියලා නැවත බලන්න CanExecuteChanged notification එක දෙනවා.

Class එක `partial` වෙන්නේ generated code එකට ඒ class එකේ ඉතිරි කොටස ලියන්න ඉඩ දෙන්න. ඔයාගේ code එකෙන් value වෙනස් කරනකොට **Title = ...** කියන්න; private field එකට value දැම්මොත් notifications මඟහැරෙනවා.

Course එකේ style එක field syntax. මේ solution එකට newer partial-property syntax හෝ LangVersion preview එක අවශ්‍ය නැහැ.

## 6. RelayCommand සහ Save button එක

```csharp
[RelayCommand(CanExecute = nameof(CanSave))]
private async Task SaveAsync()
```

Toolkit එක මේ method එකෙන් **SaveCommand** හදනවා. Async කියන කොටස command නමෙන් ඉවත් වෙනවා. XAML එකේ button එක bind කරන්නේ SaveCommand එකට.

CanSave method එක true වෙන්නේ title එක හිස් නොවී characters 40ට අඩු හෝ සමාන වෙලා, category එක තෝරලා, priority 1–5 අතර තිබුණොත්. Notes වලටත් characters 1000 limit එකක් තියෙනවා. Async command එක run වෙන අතරතුර එකම command එක නැවත run වීමත් toolkit එක නවත්වනවා.

SaveAsync එකේත් guard එකක් තියෙනවා. UI එක bypass කළත් data layer එක වැරදි title/priority/category reject කරනවා.

## 7. Task එකක් save වෙන data flow එක

1. User form එක පුරවනවා. Bindings වලින් ViewModel properties update වෙනවා.
2. Save button එක SaveCommand → SaveAsync call කරනවා. Title/Notes trim කරනවා.
3. ViewModel එක `ITodoRepository.SaveAsync` ඉල්ලනවා. Actual object එක TodoRepository.
4. TodoRepository → TodoMapper → TodoDataService හරහා database row එක සකස් කරනවා.
5. Data service එක factory එකෙන් අලුත් AppDbContext එකක් ගන්නවා. EF Core එක INSERT හෝ UPDATE කරලා SaveChangesAsync කරනවා.
6. අලුත් Id එක screen model එකට ලැබෙනවා. Save සාර්ථක වුණාට පස්සේ editor එකෙන් ආපසු යනවා.

මේ පාර code එකේ ඔයාට follow කරන්න පුළුවන් වෙන්න ඕන. Sir “Save ඔබද්දී මොකක්ද වෙන්නේ?” ඇහුවොත් මේ steps explain කරන්න.

## 8. DbContext, factory සහ migrations

`Data/AppDbContext.cs` කියන්නේ C# objects සහ database tables අතර EF Core එක භාවිතා කරන context එක. DbSet properties දෙකෙන් Todos සහ Categories tables නියෝජනය වෙනවා.

DbContext එක දිගටම singleton එකක් විදිහට තියාගෙන එකවර queries කිහිපයක් run කරන්නේ නැහැ. `IDbContextFactory` එකෙන් operation එකකට අලුත් context එකක් ගන්නවා. `await using` ඉවර වුණාම ඒ context එක dispose වෙනවා.

`DatabaseInitializer.InitializeAsync` පළමු data request එකේ migrations apply කරනවා. SemaphoreSlim gate එක නිසා requests දෙකක් එකවර ආවත් database initialization එක එකවර එකක් විතරක් වෙනවා. Failure වුණොත් initialized flag එක true නොවන නිසා ආයෙත් උත්සාහ කරන්න පුළුවන්.

Migration එකක් කියන්නේ schema වෙනස් කරන්න ලියා ඇති පියවර. InitialCreate එකෙන් tables, keys සහ categories හදනවා. `Up` ඉදිරියට වෙනස කරනවා; `Down` ආපසු ගෙනියනවා. Snapshot එකේ current EF model එක තියෙනවා.

## 9. Top 3 තෝරන LINQ එක

`Core/Rules/TodoRules.cs` එකේ GetTopThree බලන්න.

```csharp
items.Where(t => !t.IsDone)
     .OrderByDescending(t => t.Priority)
     .ThenBy(t => t.DueDate)
     .ThenBy(t => t.Id)
     .Take(3)
     .ToList();
```

Actual code එකේ ordering එක Sort method එකේ තිබෙන නිසා එය නැවත නැවත ලියලා නැහැ. ඉහත code එක ඒ සම්පූර්ණ අදහස පෙන්නනවා.

- Where: ඉවර නැති tasks විතරයි.
- OrderByDescending: 5, 4, 3... විදිහට priority වැඩි එක මුලින්.
- ThenBy DueDate: priority සමාන නම් කලින් due වෙන එක මුලින්.
- ThenBy Id: දෙකම සමාන නම් order එක ස්ථිරව තබන අමතර tie-break එක.
- Take(3): මුල් tasks තුන විතරයි; අඩුවෙන් තිබුණොත් තියෙන ගණන.
- ToList: query එක execute කරලා results list එකක් ලබාගන්නවා.

Due-date sorting එකේ due date මුලින් බලනවා. ඒ නිසා Home top 3 order එක සහ user තෝරන due-date order එක වෙනස් වෙන්න පුළුවන්.

## 10. ObservableCollection එක අවශ්‍ය ඇයි?

List එකකට item එකක් add කළා කියලා UI එකට ස්වයංක්‍රීයව දැනුම් දෙන්නේ නැහැ. ObservableCollection එක add/remove/clear notifications දෙනවා.

Collection එකේ membership වෙනස් වීම සහ එක item එකේ IsDone වෙනස් වීම වෙනස් notifications දෙකක්. TodoItem එක ObservableObject වීමෙන් IsDone වෙනසත් row එකට දැනගන්න පුළුවන්. ඒ නිසා Done button එක Reopen වෙලා title එක strike-through වෙනවා.

## 11. REST API සහ JSON

`Core/Services/OpenMeteoWeatherService.cs` බලන්න. HttpClient එක Open-Meteo endpoint එකට GET request එකක් යවනවා. Latitude/longitude Vaasa සඳහා fixed.

`GetFromJsonAsync<OpenMeteoResponse>` response JSON එක C# objects වලට map කරනවා. `JsonPropertyName` attributes වලින් JSON එකේ temperature_2m වගේ නම C# Temperature property එකට සම්බන්ධ කරනවා.

Wind request එකේ **wind_speed_unit=ms** තියෙනවා. Default km/h අගයක් 5 m/s threshold එකත් එක්ක සසඳන්න බැහැ. Response එකේ units තහවුරු කරන නිසා unexpected unit එකක් ආවොත් වැරදි kite alert එකක් පෙන්වන්නේ නැහැ.

`WeatherRules.IsKitesurfWeather` එකේ condition එක **wind > 5**, **wind >= 5** නෙවෙයි. 5.00 නම් false; 5.01 නම් true. Weather අගයන් නැති හෝ invalid නම් error path එකට යනවා.

## 12. Async, loading සහ errors

await භාවිතා කරන්නේ network/database එකෙන් response එනකම් UI thread එක block නොකරන්න. `.Result` හෝ `.Wait()` මෙතන භාවිතා කරලා නැහැ.

HomeViewModel එකේ local tasks සහ weather loading දෙක වෙන වෙනම catch කරනවා. ඒ නිසා internet නැති වුණත් database tasks load වෙනවා. Weather failure එකේ පෙර weather reading එක current කියලා පෙන්වන්නේ නැහැ. Error message එකක් සහ Refresh action එක තියෙනවා.

IsBusy/IsWeatherLoading properties වලින් loading indicators පෙන්වනවා. finally blocks වලින් failure එකක් ආවත් loading flags reset කරනවා.

UI boundary එකේ error එක userට useful message එකක් කරනවා; exception detail Debug output එකේ තියෙනවා. `async void` තියෙන්නේ MAUI lifecycle overrides වල විතරයි; business methods Task return කරනවා.

## 13. Dependency injection සරලව

`MauiProgram.cs` එකේ registrations බලන්න. ViewModel එක service එක `new` කරන්නේ නැහැ. Constructor parameter එකෙන් ඉල්ලනවා. DI container එක registered implementation එක දෙනවා.

ITodoRepository interface එක නිසා actual SQLite repository එක වෙනුවට test එකක FakeTodoRepository එකක් දාන්න පුළුවන්. IWeatherService එකටත් එහෙමයි.

**Singleton:** app එක පුරා shared service object එකක්. Repository, data service, HttpClient මේ විදිහට තියෙනවා. ඒවා page එකේ draft fields තියාගන්නේ නැහැ.

**Transient:** container එකෙන් ඉල්ලනකොට අලුත් object එකක්. Editor එකේ පරණ task එකේ draft එක අලුත් task එකට පැමිණෙන්න බැරි වෙන්න pages/VMs මෙහෙම register කරලා තියෙනවා. Shell එක current tab pages retain කරන නිසා tab එකට ආපහු එනකොට OnAppearing වලින් data reload කරනවා.

Singleton repository එකක් තිබුණත් DbContext එක singleton එකක් නෙවෙයි. ඒ වෙනස explain කරන්න පුළුවන් වෙන්න ඕන.

## 14. Navigation parameter එක යන්නේ කොහොමද?

Task row එකේ Edit button එකෙන් selected TodoItem එක EditCommand එකට ලැබෙනවා. ViewModel එක navigation service එකට task Id එක දෙනවා.

`MauiNavigationService` → Shell.GoToAsync → TodoId parameter. `AddTodoPage.ApplyQueryAttributes` ඒ අංකය අරගෙන ViewModel එකේ LoadAsync වෙත දෙනවා. ViewModel එක repository එකෙන් ඒ task එක අරගෙන form එක පුරවනවා.

අලුත් task එකකට Id එකක් යවන්නේ නැහැ. එකම editor page එක create සහ edit දෙකටම පාවිච්චි කරනවා.

## 15. Converter එක සහ row command binding එක

`PriorityToColorConverter` priority 4–5 නම් warm colour එකක්, අනෙක් priority වලට teal colour එකක් දෙනවා. Model එකේ Color property එකක් දාලා නැහැ; presentation choice එක View layer එකේ.

CollectionView row එකේ BindingContext එක TodoItem. EditCommand තියෙන්නේ TodoListViewModel එකේ. ඒ නිසා row button එක `RelativeSource` මඟින් parent ViewModel එකේ command එකට යනවා. CommandParameter එකෙන් row එකේ task object එක යවනවා.

## 16. Tests ටික බලන්නේ කොහොමද?

Visual Studio → **Test → Test Explorer → Run All**. නැත්නම් solution folder එකේ terminal එකෙන් README එකේ dotnet test command එක run කරන්න.

| Test file | පරීක්ෂා කරන අදහස |
| --- | --- |
| TodoRulesTests | Completed tasks exclude වෙනවද, priority/date order එක හරිද |
| WeatherTests | 5 m/s boundary, JSON mapping, wrong units සහ API errors |
| HomeViewModelTests | Offline වුණත් tasks එනවද, refresh/reload හරිද |
| EditorViewModelTests | Empty title, edit, failure recovery සහ property notifications |
| TodoListViewModelTests | Filters, sort orders, cancelled delete සහ edit ID |
| DatabaseTests | Real SQLite migrations, persistence, CRUD සහ category relation |

Test double එක කියන්නේ actual external dependency එක වෙනුවට අපි control කරන object එකක්. Weather tests වල internet එකට depend නොවී response එක අපි තීරණය කරනවා. Database tests වල ඇත්ත SQLite file එකක් භාවිතා කරනවා.

## 17. ඔයාටම කරලා බලන්න වැඩ තුනක්

1. App එකේ heading එකක් වෙනස් කරලා run කරන්න. ඒක pure View change එකක් කියලා explain කරන්න.
2. Test එකක priority values වෙනස් කරලා expected order එක ඔයාම කලින් ලියන්න. පස්සේ test run කරන්න.
3. Temporary kite limit එක 5 සිට 6 කරන්න. Boundary tests දෙකත් ඒ අනුව වෙනස් කරලා run කරන්න. Practice ඉවර වුණාම assignment requirement වන 5ට ආපසු හදන්න.

මේ වෙනස්කම් කරද්දී තේරෙන්නේ නැති method එකක් හම්බ වුණොත් ඒ file එකෙන්ම පටන් ගන්න. මුළු code එකම කටපාඩම් කරනවාට වඩා data flow එක තේරුම් ගන්න එක වැදගත්.
