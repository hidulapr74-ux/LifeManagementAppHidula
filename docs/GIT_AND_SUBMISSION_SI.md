# Git එකට දාලා submit කරන විදිහ

## 1. Repository access එක

Course pages අතර GitHub private/public ගැන වෙනස් instructions තියෙනවා. එකඟ instruction එක වන්නේ **VAMK Git private repository එකක් නම් teacherගේ `tuo` account එක collaborator කරන්න** කියන එක. Actual submission page එක GitHub ඇතුළු වෙනත් services වල repo public විය යුතුයි කියනවා.

Repository එකක් මේ project සකස් කරන අතරතුර ඔයාගේ account එකේ create කරලා හෝ publish කරලා නැහැ. Moodle submission එකත් කරලා නැහැ.

## 2. VAMK Git empty repository එකක් හදන්න

Browser එකෙන් ඔයාගේ VAMK Git account එකේ New Repository තෝරන්න. Name එක `LifeManagementApp` වගේ දෙන්න. Default branch එක **main** කරන්න. README, licence, gitignore initialize කරන්න එපා; මෙතන ඒ files දැනටමත් තියෙනවා.

Private නම් teacher `tuo` collaborator කරන්න. Repository එකේ HTTPS URL එක copy කරගන්න.

## 3. Download එකේ local history එක බලන්න

ZIP එකේ genuine preparation commits සහ feature branches සහිත `.git` folder එක ඇතුළත් වෙනවා. Commits studentගේ නමින් හදලා නැහැ; Project assistant කියලා පැහැදිලිව තියෙනවා.

Solution folder එකේ PowerShell terminal එකෙන්:

```powershell
git status
git log --oneline --graph --all
git remote -v
```

Remote එකක් තවම නැහැ. ඔයාගේ actual repository URL එක දාලා:

Preparation එකේ local author setting එක ZIP එකෙන් ඉවත් කරලා තියෙන නිසා ඔයාගේ computer එකේ Git identity එක භාවිතා වෙනවා. `git config --get user.name` සහ `git config --get user.email` වලින් බලන්න. ඒවා සකස් කරලා නැත්නම් ඔයාගේ නම සහ Git account එකේ email එක local repo එකට configure කරලා පස්සේ own commits කරන්න.

```powershell
git remote add origin YOUR_REPOSITORY_URL
git push -u origin main
```

YOUR_REPOSITORY_URL කියන text එක 그대로 run කරන්න එපා; ඔයා copy කරපු actual URL එක දාන්න. “origin already exists” ආවොත් `git remote -v` බලලා existing remote එක හරිද තහවුරු කරගන්න.

ඔයාම කරලා බලපු සැබෑ code changes commit කරන්න. පරණ commits backdate කරන්න හෝ AI-generated preparation work එක ඔයාම ලියපු එකක් වගේ author වෙනස් කරන්න අවශ්‍ය නැහැ. README එකේ AI use section එක ඇත්තට ගැළපෙන්න තියෙන්න දෙන්න.

## 4. B1 bonus එකට ඉතිරි evidence

Local branches විතරක් තිබීම B1 සඳහා ප්‍රමාණවත් නැහැ. Hosted repository එකේ actual issue එකක්, ඒ issue එකට අදාළ feature branch එකක් සහ issue එක reference කරන merge/PR එකක් අවශ්‍යයි.

අලුතින් කරන සැබෑ feature එකක් issue එකක ලියලා පටන් ගන්න. උදාහරණයක් විදිහට task row එකේ notes පෙන්වීම. Issue number එක site එකෙන් ලැබුණු පසු ඒ actual number එක භාවිතා කරන්න. Fake issue numbers දාන්න එපා.

```powershell
git switch main
git pull
git switch -c feature/show-task-notes
```

Change එක කරලා run කරලා පස්සේ actual issue number එක commit/PR description එකේ reference කරන්න. PR merge කළාට පස්සේ main එකට යන්න. මේ project එකට hosted issues හෝ PRs තවම create කරලා නැහැ.

## 5. Submit කිරීමට පෙර

1. App එක ඔයාගේ laptop එකේ run කරලා manual checks සම්පූර්ණ කරන්න.
2. Required changes main branch එකට merge කරලා push කරන්න.
3. README එකේ AI use, tested platform සහ known issues ඇත්තට ගැළපෙන බව බලන්න.
4. Teacherට repository එක open කරන්න පුළුවන් බව check කරන්න.
5. `git log -1 --format=%h` command එකෙන් current short commit hash එක ගන්න.

Actual Moodle submission page එකේ දෙන fields:

```text
Git link:          [ඔයාගේ actual repo URL]
Last commit:       [actual short hash]
Target grade:      [ඔයා අරමුණු කරන grade එක]
Tested on:         [ඔයා ඇත්තටම run කරපු platform එක]
Persistence:       SQLite + EF Core
Bonuses:           B1: [actual issue/PR evidence තිබේ නම් yes]
                   B2: complete/edit/delete; categories and filtering
                   B3: yes, xUnit tests
Known issues:      [ඉතිරි සැබෑ issues]
```

**Deadline: 22 November 2026, 23:59**, portal එකේ පෙන්වන වෙලාව. Submitted commit එක assessment සඳහා භාවිතා කරනවා කියලා actual submission activity එකේ තියෙන නිසා finished version එක submit කරන්න. පස්සේ කරපු වෙනස්කම් automatically marks වලට ගනී කියලා හිතන්න එපා.
