Feature: Rates
	Get rates from OpenExchangeRates api

@mytag
Scenario: Rates list is not empty when requesting data 
	Given a user is using the OpenExchangeRates api
	When the user requests all rates
	Then a non empty list with rates is displayed