Questions I would ask and assumptions made on those questions:

1. Data Questions
	a. Should we have a table and validations to check that states, zipcodes, etc are valid
	b. Phone Number Validation
	c. Balance is a string in the sample data, should it be stored internally as a decimal and reported out as a formatted string?
	d. Longitude and latitude are formatted as strings in the sample data. Should we keep these as decmials internally?
	e. Registered is a formatted date, this would be better served as a date type internally
	f. Are tags freeform or from a predefinied list
	g. Should address be broken into a more normalized form
	h. Should eye color come from a predefined list 
	i. Guid should be a Guid
	j. customer with id 9848 is duplicated. are we expected to match use GUID as primary ID? 
	k. should we use a repository Pattern to enable easy switching of data sources and logic?
	l. are customers dependant on agents? 
	m. UI claims that we will show City and state for customers but city and state are not given.
	n. Return all customer data is under Agent's Customer detail. should I assume this is customers for that agent only?

Data Question Answers - Assumptions Used
1.	
	a. Probably should be but that is fairly trivial to this Proof of concept and will be ignored
	b. Standard Phone Validation shoudl be ok without more context
	c. Balance should be able to be submitted in either formatted string or decimal form and stored as a decimal 
	d. Longitude and latitude shoudl be reported out as string but stored as decimals
	e. Registered should be accepted and preferred as a JSON date type but will be accepted as the formatted string given. and reported out in teh same format
	f. Tags will coem from a predefined List
	g. Address should be more normalized but will not be for this exercise
	h. Eye color should come from a list but will not for this exercise
	i. Yes, Guid should be a Guid
	j. Let's assume this is just a data error and correct the second 9848 to 9849 
	and that ID will be our primary ID on the DB going forward.
	k. probably but that seems out of scope for this test. 
	For now EF will function as a fine Repository that we can connect to an alternate data source with such simple logic. 
	l. for this exercise let's assume they are completely dependant in the real world you would probably be able to change teh agent a customer is associated with.
	m. for now we will prevoide the location data we have, address and lat/long
	n. I am assuming that the call only wants to return all customers from a particular agent 
