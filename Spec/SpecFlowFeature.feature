Feature: SpecFlowFeature

Scenario: Go to site from the google
	When I navigate to Google
	And I search rw.by
	And I press Search
	And I find result in the list
	And I open link with search criteria
	Then The site is successfully loaded

Scenario: Change language on the main page
	Given I am on rw.by page
	When I switch language
	Then Language changed

Scenario: Check number of news on the main page
	Given I am on rw.by page
	When I get number of news
	Then Number of news is 6