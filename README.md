# Дипломна работа за завършване на 12ти клас в ТУЕС към ТУ-София.

## Документация на дипломната работа
https://drive.google.com/file/d/1ds3Pz0Ge8lHulwjZXMFRv7BfyfwjbEjr/view?usp=sharing

## Ръководство за потребителя

### Инсталация на играта
Инсталационния файл на дипломната работа се намира в Github репозиторито под “Releases”. За да го свалите отидете на следния линк: github.com/RayaLitova/Diplomna. В дясната част на екрана се намира секцията “Releases”. При натискане върху последната версия на проекта, намираща се точно под заглавието на секцията, ще бъдете изпратени до страница, показваща файловете, които се съдържат в конкретната версия. Натиснете върху файла “SetupDiplomna.exe” и разрешете свалянето му. Това е инсталационния файл. Когато свалянето завърши, навигирайте до директорията, в която се намира, и го отворете. Това ще стартира инсталацията. Следвайте стъпките на програмата до приключване.

### Инсталация на работния проект
За инсталация на целия Github проект трябва да се свали бранча “Final-version-22.02”. В него се съдържат всички работни файлове на дипломната работа. Това включва множество активи, които не са използвани в настоящата версия на проекта. Общо за инсталацията на целия проект са необходими около 10 гигабайта свободно място на диска. 
Изтегляно се изпълнява чрез git терминал. За целта първоначално трябва да имате свален git. Той представлява безплатен софтуер за контрол на версиите, използван както от любители, така и от големи компании. Той може да бъде свален от този линк (git-scm.com/downloads). След като имате подготвен git навигирайте до директорията, в която искате на инсталирате проекта, и отворете git bash чрез десен бутон -> “Git Bash Here”. След това въведете следната команда, която ще свали целия проект в настоящата директория:
git clone –single-branch –branch Final-version-22.02 https://github.com/RayaLitova/Diplomna.git

За да бъде отворен работния проект е необходим Unity Editor версия 2021.3.10f1. Отворете Unity Hub и изберете опцията “Open”. Тя ще отвори прозорец, в който трябва да навигирате до директорията, в която е запазен проекта. Когато сте готови, натиснете “Open”. Това ще зареди проекта в Unity Editor. От там чрез бутона за стартиране може да пуснете играта.

В основната директория на работния проект се съдържат 4 основни директории - “Assets”, “Packages”, “ProjectSettings” и “UserSettings”. В директорията “Assets“ се намират всички работни файлове на играта. В директорията “Packages” могат да бъдат намерени два .json файла, съдържащи всички пакети, които са необходими за стартиране на проекта. В папките “ProjectSettings” и “UserSettings” се съдържат всички настройки по проекта и едитора, направени от разработчика. 
“Assets” директорията има множество подразделения, като са групирани на типове. Из цялата директория има голямо количество работни файлове, които не са вкарани в употреба в сегашната версия на дипломната работа, което утежнява проекта допълнително.

### Стартиране на играта
След изтегляне на инсталационния файл и завършването на инсталационния процес в избраната от вас директория ще се намира файл с име “Diplomna.exe”. При неговото отваряне играта се стартира.

### Контроли
Когато героят влезе в сцена, в която можете да го контролирате, трябва да използвате бутоните на клавиатурата “W”, “A”, “S” и “D”, за да го придвижвате в четирите посоки. С преместване на мишката се движи и камерата. По този начин може да оглеждате околността около героя си. Движението на героя е релативно на посоката на камерата, което означава, че посоката, в която гледа камерата, винаги ще отговаря на бутона “W”. 
Освен нормалното движение героя има и функцията “Dash”, която му позволява по-бързо движение в една посока на всеки 3 секунди. Тя се изпълнява с клавиша “Shift” натиснат по време на движение. 
През цялото ниво курсора на мишката е невидим, за да го отключите трябва да задържите клавиша “Alt”. Това е необходимо при поставяне и преместване умения и предмети.

### Механики на играта
Докато героят се намира в подземието, той може да се движи, да побеждава опоненти с предварително избрани умения, да променя и събира магически предмети и да търси стаята с елитния противник, за да отключи портала, през който да излезе от подземието.
Избирането на уменията се извършва в началната безопасна стая, чийто край е означен с полупрозрачна синя стена. От менюто с умения, което се отваря с клавиш “K”, с отключване на курсора могат да се преместват умения от менюто с лентата за действия намираща се в долната лява част на екрана. Уменията могат да се изпълняват с клавишите “Q”, “E” и “R”. 
Има три вида умения - умения, нанасящи щети на противниците, умения, даващи бонуси на играча и умения, които са комбинация от другите два вида. Когато играчът изпълни умение, нанасящо щети, в достатъчна близост до противник, той понася определено количество щети, които намаляват точките му живот. Когато играчът изпълни умение, което му дава бонус, той получава индикация чрез червени линии, обикалящи героя. Повечето бонуси важат до следващо изпълняване на умение, нанасящо щети.

Магическите предмети дават пасивни бонуси на героя. Всеки предмет има различно действие, като има предмети, които подобряват определен атрибут като атака, време за охлаждане на уменията или регенерация, и такива, които подобряват един стат, но намалят друг. Причината, поради която бихте искали да изберете предмети, които имат и лошо действия, е, че техния бонус е по-голям от този на предметите с едно действие. Те могат да бъдат събирани чрез убиване на нормални противници и могат да бъдат променяни по всяко време. Всички притежавани и непритежавани магически предмети могат да бъдат видяни в инвентара, който се отваря с клавиша “I”. За да бъде активиран един магически предмет той трябва да бъде поставен в лентата с предмети, който се намира в горния ляв ъгъл на екрана. Когато един противник бъде победен и отключи предмет, получавате индикация чрез светещ син ромб, изскачащ над главата на героя. В края на всяко подземие магическите предмети се губят и играчът трябва да ги събира отново.

По средата на лентата за действия се намира живота на героя. През цялото подземие трябва да следите дали не са близо до нулата, защото, когато точките живот на героя достигнат 0, той умира. Ако точките живот на героя са прекалено малко може да изпълните умение, даващо живот на героя, да активирате предмет, даващ допълнителна регенерация, или да изчакате естествената регенерация. 

Героят има функционалност “Взаимодействие”, която му позволява да изпълнява различни действия, като си взаимодейства с различни предмети, които му дават тази опция. Индикацията за възможност за взаимодействие е текст, който описва действието, което може да бъде изпълнено. Взаимодействията се изпълняват с клавиш “F”. 
В началото на всяко подземие се появява анимация, указваща пътя на героя до елитния противник. Тази сцена, както и всяка друга анимация, може да се пропусне чрез натискане на клавиша “Esc”. Ако пътя на героя е възпрепятстван от затворена врата, трябва да победите всички нормални противници в дадената стая, за да се отвори.

### Начално меню
Първата сцена след стартиране на играта е сцената с началното меню. В нея имате две опции - стартиране на нова игра и излизане от играта. При стартиране на нова игра играчът е изпратен в сцената с обучителното ниво.

### Сцена с обучително ниво
В началото на сцената с обучителното ниво обучителен текст ще ви въведе в механиките на играта. Те се показват от лявата страна на екрана и могат да се преминават с клавиш “Space” или да се прекъснат с натискане на бутона за затваряне в дясната част на текстовото поле. След като бъдете въведени в играта започва вашето първо подземие. За да преминете успешно подземието, трябва да откриете и победите елитен противник. Побеждаването му ще стартира анимация за отключване на портал, който ще трябва да откриете, за да излезете успешно от подземието. По време на пътя си през подземието ще трябва да побеждавате нормални противници, за да събирате магически предмети и да отключвате врати. Това ще ви позволи да достигнете и победите финалния противник и успешно да излезете от подземието.

### Сцена с град
След успешно излизане от подземието или смърт на героя по време на преминаването му ще бъдете изпратени в главния град. При първо влизане в града ще бъдете посрещнати със същите въвеждащи текстове от обучителното ниво, но с променено съдържание. В града имате опцията да обикаляте и разглеждате околностите или да влезете в таверната, намираща се зад кладенеца на главния площад.

### Сцена с таверна
При влизане в таверната имате единствени опции да говорите с бармана, което ще ви изпрати в следващото ниво на подземието, или да излезете.

### Подземия и цел на играта
Всяко следващо подземие съдържа повече противници, което го прави по трудно за преминаване. Целта на играта е успешно преминаване на 10 нива на подземието.






