Feature: Add profile details
	As a Seller
	I want the feature to add my Profile Details
	So that the people seeking for some skills can look into my details.

Background:
	Given I login to the website

#### Happy day scenarios (functionality) START ####
#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding language details with valid data
	When I save 'Language' details as follows:
		| Name      | Level  |
		| Something | Fluent |
	Then my profile page displays the newly added 'Language' details
	And the success message "Something has been added to your languages" is displayed

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding education details with valid data
	When I save 'Education' details as follows:
		| InstituteName          | Country     | DegreeTitle | DegreeName          | YearOfGraduation |
		| University of Auckland | New Zealand | B.Sc        | Bachelor of Science | 2000             |
	Then my profile page displays the newly added Education details
	And the success message "Education has been added" is displayed

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding certification details with valid data
	When I save 'Certification' details as follows:
		| Name             | Organisation | Year |
		| Foundation Level | ISTQB        | 2020 |
	Then my profile page displays the newly added 'Certification' details
	And the success message "Foundation Level has been added to your certification" is displayed

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding skills details with valid data
	When I save 'Skill' details as follows:
		| Name     | Level  |
		| Selenium | Expert |
	Then my profile page displays the newly added 'Skill' details
	And the success message "Selenium has been added to your skills" is displayed

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding profile description with valid data
	When I save the the following description
		"""
		I'm a tester. Please contact me for testing skills.
		"""
	Then my profile page displays the newly added 'Description' details
	And the success message "Description has been saved successfully" is displayed

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding profile photo
	When I upload a photo
	Then my profile displays the newly uploaded profile photo
	And the success message "Profile phooto has been uploaded successfully" is displayed

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Adding location
	When I save location as "Auckland, New Zealand"
	Then my profile displays the newly added location
	And the success message "Location has been saved successfully" is displayed

### what about name and other fields on the left? Is it considered updating since they were already pre-populated? ###
#### Happy day scenarios END ####

#### Happy day scenarios (functionality) END ####

#### Field Validations START ####
#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@runTest
Scenario: Adding more than 4 language details are not allowed
	Given I already have 3 'Language' details as follows:
		| Name    | Level  |
		| Spanish | Fluent |
		| French  | Fluent |
		| German  | Fluent |
	When I save another 'Language' details as follows:
		| Name    | Level  |
		| English | Fluent |
	Then my profile page displays the newly added 'Language' details
	And the success message "English has been added to your languages" is displayed
	But I cannot add another 'Language' detail

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
# The below test actually does not work as not all validations are in place
@runTest
Scenario Outline: Adding language details with invalid data
	Given I already have 1 'Language' details as follows:
		| Name    | Level  |
		| English | Fluent |
	When I add language detail with name '<Name>' and level '<Level>'
	Then the error message '<Error>' is displayed
	And the detail is not saved

	Examples:
		| Name          | Level            | Error                                                 |
		|               | Fluent           | Please enter language and level                       |
		| German        |                  | Please enter language and level                       |
		| ''English  '' | Basic            | Please enter language and level                       |
		| '' English''  | Basic            | Please enter language and level                       |
		| '' ''         |                  | Please enter language and level                       |
		| @#$           | Native/Bilingual | Please enter language and level                       |
		| 12345         | Native/Bilingual | Please enter language and level                       |
		| English       | Conversational   | Duplicated data                                       |
		| English       | Fluent           | This language is already exist in your language list. |

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario Outline: Adding skill details with invalid data
	Given I already have 1 'Skill' details as follows:
		| Name    | Level    |
		| Cypress | Beginner |
	When I add skill detail with name '<Name>' and level '<Level>'
	Then the error message '<Error>' is displayed
	And the detail is not saved

	Examples:
		| Name          | Level        | Error                                           |
		|               | Intermediate | Please enter skill and experience level         |
		| Cypress       |              | Please enter skill and experience level         |
		| ''Cypress  '' | Beginner     | Please enter skill and experience level         |
		| '' Cypress''  | Beginner     | Please enter skill and experience level         |
		| '' ''         |              | Please enter skill and experience level         |
		| @#$           | Beginner     | Please enter skill and experience level         |
		| 12345         | Beginner     | Please enter skill and experience level         |
		| Cypress       | Expert       | Duplicated data                                 |
		| Cypress       | Beginner     | This skill is already exist in your skill list. |

#-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario Outline: Adding education details with invalid data
	Given I already have 1 'Education' details as follows:
		| InstituteName          | Country     | DegreeTitle | DegreeName          | YearOfGraduation |
		| University of Auckland | New Zealand | B.Sc        | Bachelor of Science | 2000             |
	When I add education detail with institute '<InstituteName>', country '<Country>', title '<title>', name '<DegreeName>', and graduation year '<YearOfGraduation>'
	Then the error message '<Error>' is displayed
	And the detail is not saved

	Examples:
		| InstituteName               | Country     | DegreeTitle | DegreeName               | YearOfGraduation | Error                                           |
		|                             | New Zealand | B.Sc        | Bachelor of Science      | 2000             | Please enter all the fields                     |
		| University of Auckland      |             | B.Sc        | Bachelor of Science      | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand |             |                          | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | Bachelor of Science      |                  | Please enter all the fields                     |
		| ''University of Auckland '' | New Zealand | B.Sc        | Bachelor of Science      | 2000             | Please enter all the fields                     |
		| '' University of Auckland'' | New Zealand | B.Sc        | Bachelor of Science      | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | ''Bachelor of Science '' | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | '' Bachelor of Science'' | 2000             | Please enter all the fields                     |
		| '' ''                       | New Zealand | B.Sc        | Bachelor of Science      | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | '' ''                    | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | ''Bachelor of Science '' | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | '' Bachelor of Science'' | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | '' ''                    | 2000             | Please enter all the fields                     |
		| 12345                       | New Zealand | B.Sc        | Bachelor of Science      | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | 12345                    | 2000             | Please enter all the fields                     |
		| University of Auckland      | New Zealand | B.Sc        | Bachelor of Science      | 1999             | Duplicated data                                 |
		| University of Auckland      | New Zealand | B.Sc        | Bachelor of Science      | 2000             | This skill is already exist in your skill list. |
		| @#$                         | New Zealand | B.Sc        | Bachelor of Science      | 2000             | Please enter valid values for all the fields    |
		| University of Auckland      | New Zealand | B.Sc        | #@$                      | 2000             | Please enter valid values for all the fields    |

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario Outline: Adding certification details with invalid data
	Given I already have 1 'Certification' details as follows:
		| Name             | Organisation | Year |
		| Foundation Level | ISTQB        | 2020 |
	When I add certification detail with name '<Name>', level '<Organisation>', and year '<Year>'
	Then the error message '<Error>' is displayed
	And the detail is not saved

	Examples:
		| Name                  | Organisation | Year | Error                                                                            |
		|                       | ISTQB        | 2020 | Please enter Certification Name, Certification From and Certification Year       |
		| Foundation Level      |              | 2020 | Please enter Certification Name, Certification From and Certification Year       |
		| Foundation Level      | ISTQB        |      | Please enter Certification Name, Certification From and Certification Year       |
		| @#$                   | ISTQB        | 2020 | Please enter Certification Name, Certification From and Certification Year       |
		| Foundation Level      | @#$          | 2020 | Please enter Certification Name, Certification From and Certification Year       |
		| 12345                 | @#$          | 2020 | Please enter Certification Name, Certification From and Certification Year       |
		| Foundation Level      | 12345        | 2020 | Please enter Certification Name, Certification From and Certification Year       |
		| Foundation Level      | ISTQB        | 2020 | This information is already exist.                                               |
		| Foundation Level      | ISTQB        | 2019 | Duplicated data                                                                  |
		| ''Foundation Level '' | ISTQB        | 2020 | Please enter valid Certification Name, Certification From and Certification Year |
		| '' Foundation Level'' | ISTQB        | 2020 | Please enter valid Certification Name, Certification From and Certification Year |
		| Foundation Level      | ''ISTQB ''   | 2020 | Please enter valid Certification Name, Certification From and Certification Year |
		| Foundation Level      | '' ISTQB''   | 2020 | Please enter valid Certification Name, Certification From and Certification Year |

#### Field Validations END ####

### Miscellaneous START ###
#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Tests below actually do not pass as thers is a bug on the application which allows this incorrect behaviour to occur
@runTest
Scenario: Adding language details with valid data on multiple tabs
	Given I have opened the profile page on another tab
	And I already have 1 'Language' details as follows:
		| Name    | Level  |
		| Spanish | Fluent |
	And I switch back to the previous tab
	When I save another 'Language' details as follows:
		| Name   | Level  |
		| German | Fluent |
	Then the error message 'Please refresh the page.' is displayed
	And the detail is not saved

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
@ignore
Scenario: Long texts are truncated and full texts are shown on hover
	Given I have added a skill with name 'The name of this skill is very long for no reason and should be truncated' and level 'Beginner'
	When I hover over the truncated skill name
	Then I can see the tooltip with the full text "The name of this skill is very long for no reason and should be truncated"

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
#@ignore
#Scenario: Upating deleted language details
#	Given I already have 1 'Language' details as follows:
#		| Name    | Level  |
#		| Spanish | Fluent |
#	And I have opened the profile page on another tab
#	And I deleted that 'Language' detail
#	And I switch back to the previous tab
#	When I update 'Language' details as follows:
#		| Name   | Level  |
#		| German | Fluent |
#	Then the error message 'This record has already been deleted.' is displayed
#	And the detail is not saved

#-------------------------------------------------------------------------------------------------------------------------------------------------------------------
#@ignore
#Scenario: Deleting deleted language details
#	Given I already have 1 'Language' details as follows:
#		| Name    | Level  |
#		| Spanish | Fluent |
#	And I have opened the profile page on another tab
#	And I deleted that 'Language' detail
#	And I switch back to the previous tab
#	When I delete 'Language' details
#	Then the error message 'This record has already been deleted.' is displayed
### Miscellaneous END ###