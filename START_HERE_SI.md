# මචං, project එක පටන් ගන්නේ මෙහෙමයි

මේක **LifeManagementApp** කියන C# / .NET MAUI project එක. Windows laptop එකේ Visual Studio එකෙන් run කරන්න හදලා තියෙන්නේ.

## 1. ZIP එක extract කරන්න

ZIP එක ඇතුළේ ඉඳලා project එක open කරන්න එපා. File එක right-click කරලා **Extract All** දෙන්න. උදාහරණයක් විදිහට `D:\Projects\LifeManagementApp` folder එකට දාන්න.

## 2. Visual Studio tools බලන්න

**Visual Studio Installer → Modify → Workloads → .NET Multi-platform App UI development** select කරලා තියෙන්න ඕන. **.NET 9 SDK** එකත් අවශ්‍යයි. Visual Studio 2022 නම් 17.12 හෝ ඊට පසු version එකක් භාවිතා කරන්න.

Terminal එකේ `dotnet --list-sdks` දුන්නාම `9.0...` entry එකක් තියෙනවාද බලන්න. `10.0...` එක විතරක් තිබුණොත් .NET 9 එකත් install කරන්න; course එකේ target එක 9.

## 3. Solution එක open කරන්න

Visual Studio → **Open a project or solution** → extract කළ folder එකේ **LifeManagementApp.sln** තෝරන්න.

Solution Explorer එකේ `src` යට තියෙන **LifeManagementApp** project එක right-click → **Set as Startup Project**. `Core`, `Data` හෝ `Tests` startup project එක කරගන්න එපා.

## 4. Run කරන්න

NuGet packages restore වෙලා ඉවර වෙනකම් ඉඳලා, උඩ run target එක **Windows Machine** තෝරලා **F5** ඔබන්න. පළවෙනි build එකට ටිකක් වෙලා යන්න පුළුවන්.

Database server එකක් හෝ API key එකක් දාන්න අවශ්‍ය නැහැ. App එක local SQLite database එක හදාගන්නවා. Weather බලන්න internet අවශ්‍යයි; task වැඩ offline කරන්න පුළුවන්.

## 5. මුලින් මේ පොඩි වැඩ ටික කරන්න

1. **+ New task** ඔබලා `Read C# notes` කියලා task එකක් හදන්න. Priority 5, Category Study දෙන්න.
2. තවත් tasks තුනක් විවිධ priority සහ due dates එක්ක දාන්න.
3. **My tasks** tab එකට ගිහින් priority සහ due date sorting දෙකම බලන්න.
4. Task එකක් **Done** කරලා **Today** tab එකට යන්න. ඒක top 3 එකෙන් ඉවත් වෙලා තියෙන්න ඕන.
5. App එක වහලා නැවත run කරන්න. Tasks නැති වෙන්න බැහැ.

## 6. ඉගෙනගන්න කියවන්න ඕන පිළිවෙළ

| File | ඒකෙන් කරන්නේ මොකක්ද? |
| --- | --- |
| `docs/COURSE_REQUIREMENTS.md` | Teacherගේ requirements, marks, deadline සහ වෙනස් instructions |
| `docs/EXPLAIN_CODE_SI.md` | Code එක සිංහලෙන් තේරුම් ගන්න |
| `docs/PRESENTATION_GUIDE_SI.md` | Demo එක, Q&A සහ live coding practice |
| `docs/GIT_AND_SUBMISSION_SI.md` | Git එකට push කරලා submit කරන විදිහ |
| `docs/VERIFICATION.md` | කරලා තියෙන checks සහ laptop එකේ කරන්න ඉතිරි checks |

## 7. මතක තියාගන්න

**Git link deadline:** 2026 November 22, 23:59 — portal එකේ පෙන්වන වෙලාව.

2026 November 20 දා scheduler එකේ පෙන්වූ ඔයාගේ booking එක **December 2, 11:06–11:16, A3009**. Own laptop එක අරන් යන්න. මේ project එකේදී ඒ booking එක වෙනස් කරලා නැහැ.

App එක ලස්සනට වැඩ කළත් code එක explain කරන්න බැරි නම් marks අඩු වෙනවා. ඒ නිසා guide එක කියවලා code එකේ පොඩි වෙනස්කම් ඔයාම කරලා බලන්න.

Windows app එක මේ සකස් කළ Linux environment එකේ run කරලා නැහැ. Android build එක සහ portable tests පරීක්ෂා කරලා තියෙනවා; presentation laptop එකේ F5 කරලා manual checks ටික අනිවාර්යයෙන් සම්පූර්ණ කරන්න.
