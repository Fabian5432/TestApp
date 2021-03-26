Feature: Quates
	Get quates from CurrencyLayer api

@mytag
Scenario: Quates list is not empty when requesting data 
	Given a user is using the CurrencyLayer api
	When the user requests all quates
	Then a non empty list with quates is displayed
