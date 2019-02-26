<img align="left" src="https://raw.githubusercontent.com/kamiljaworski/AttendanceListGenerator/master/docs/img/Icon.png" alt=""/>

# Attendance list generator
*Attendance list generator* is a simple WPF desktop application that create PDF attendance list documents.

<br /><br />
## Goal
This application was created to resolve a certain problem.
I needed to create an attendance list every month and I didn't like to edit it every time in Word
so I created an application that generates these lists.

## Features

- creating an attendance list
- adding up to 7 fullnames to the list
- selecting any month of a year (after 1900 and before 2100)
- coloring saturdays, sundays and holidays
- printing that it's a sunday in this day
- printing that it's a holiday in this day
- table stretching to center the table
- saving last used settings and fullnames after generating a list

## Frameworks/Extensions

- Fody
- Fody PropertyChanged
- MigraDoc
- Newtonsoft.Json
- NUnit
- Moq

## Screenshots

![Start screen sceenshot](https://raw.githubusercontent.com/kamiljaworski/AttendanceListGenerator/master/docs/img/StartScreen.png)

![Filled start screen sceenshot](https://raw.githubusercontent.com/kamiljaworski/AttendanceListGenerator/master/docs/img/FilledStartScreen.png)

![Example generated document sceenshot](https://raw.githubusercontent.com/kamiljaworski/AttendanceListGenerator/master/docs/img/GeneratedDocument.png)
