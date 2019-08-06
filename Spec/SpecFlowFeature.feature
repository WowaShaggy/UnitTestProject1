Feature: SpecFlowFeature

@google
Scenario: Go to site from the google
	When I navigate to Google
	And I search rw.by
	And I press Search
	And I find result in the list
	And I open link with search criteria
	Then The site is successfully loaded

@main
Scenario: Change language on the main page
	Given I am on rw.by page
	When I switch language
	Then Language changed

@main
Scenario: Check number of news on the main page
	Given I am on rw.by page
	When I get number of news
	Then Number of news is 6

@main
Scenario: Check label in page bottom
	Given I am on rw.by page
	And I switch language
	When I check label in page bottom
	Then Label is "© 2019 Belarusian Railway"

@main	
Scenario: Check top buttons
	Given I am on rw.by page
	And I switch language
	When I check find top buttons
	Then Top buttons are"Timetable", "Passenger Services", "Corporate" and "Freight"

@search
Scenario: Check Search functionality negative case
	Given I am on rw.by page for search
	When I type random String with 20 characters in Search Bar
	Then Link contains this String
	And Page contains label "По Вашему запросу ничего не найдено"

@search
Scenario: Check Search functionality positive case
	Given I am on search page for random string
	When I clean Search bar
	And  I type "Санкт-Петербург" in Search Bar
	And I press Search button
	Then Page displays 10 search results
	And I bring out results in console

@calendar
Scenario: Check Calendar function and find schedule
	Given I am on rw.by page for calendar
	When I enter Locations "Брест" and "Оранчицы"
	And  I choose day in calendar in 35
	And I press Search Day button
	Then Console displays Schedule

@calendar
Scenario: Check schecule result
	Given I am on schedule page for "Брест" and "Оранчицы" and date in 35
	When I click on the first result
	Then Route is displayed
	And Calendar description isn't empty

	
@calendar
Scenario: Logo returning
	Given I am on schedule page for "Брест" and "Оранчицы" and date in 35
	And  I click on the first result
	When I click logo
	Then Main Page appears
