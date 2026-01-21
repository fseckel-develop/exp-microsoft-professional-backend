## Objective

- Analyse real-world scenarios involving sensitive data
- Decide when to apply data masking vs. data obfuscation
- Justify technique selection based on context and security requirements

---
## Example 1: E-Commerce Platform

### Scenario Overview

- Platform processes customer payment details
- Multiple security and access requirements exist across storage, access, and transmission

### Requirement Analysis

- Secure storage of payment data in database
	- Technique: Data obfuscation (e.g., tokenisation)
	- Reason:
		- Stored data becomes unreadable to unauthorised users
		- Prevents meaningful data exposure even if the database is compromised

- Limited visibility for customer service representatives
	- Technique: Data masking (dynamic masking)
	- Reason:
		- Displays only last four digits of credit card numbers
		- Supports verification without exposing full sensitive data

- Secure transmission between client and server
	- Technique: Data obfuscation (e.g., scrambling)
	- Reason:
		- Transforms data into unreadable format during transit
		- Protects against interception and misuse

### Technique Summary

- Secure database storage
	- Data obfuscation
	- Prevents interpretation of stored credit card details

- Limited customer service view
	- Data masking
	- Protects privacy while enabling partial visibility

- Secure data transmission
	- Data obfuscation
	- Prevents interception of sensitive information

---
## Example 2: Healthcare System

### Scenario Overview

- Hospital system manages patient records and Social Security numbers
- Data is used for operations, portals, and research

### Requirement Analysis

- Anonymised data for research purposes
	- Technique: Data masking (static masking)
	- Reason:
		- Replaces sensitive data with realistic but fictional values
		- Preserves data usefulness while protecting patient privacy

- Partial display of Social Security numbers in portals
	- Technique: Data masking (dynamic masking)
	- Reason:
		- Shows limited information (e.g., XXX-XX-6789)
		- Prevents full exposure while allowing verification

- Secure storage of medical records
	- Technique: Data obfuscation (e.g., tokenization)
	- Reason:
		- Makes data unreadable if accessed without authorization
		- Protects highly sensitive medical information

### Technique Summary

- Anonymised research data
	- Data masking
	- Protects privacy while enabling analysis

- Partial SSN display
	- Data masking
	- Conceals sensitive identifiers with limited visibility

- Secure database storage
	- Data obfuscation
	- Prevents unauthorised interpretation of medical records

---
## Example 3: Financial Data Protection

### Scenario Overview

- Financial institution processes bank account details and transaction records
- Data is accessed via applications, databases, and inter-server communication

### Requirement Analysis

- Concealing account numbers in customer-facing applications
	- Technique: Data masking (dynamic masking)
	- Reason:
		- Displays only the last four digits (e.g., XXXX-XXXX-1234)
		- Allows verification while protecting full account numbers

- Secure storage of transaction records
	- Technique: Data obfuscation (e.g., tokenization)
	- Reason:
		- Prevents stored financial data from being interpreted if accessed unlawfully

- Secure transmission of transaction records between servers
	- Technique: Data obfuscation (e.g., scrambling)
	- Reason:
		- Protects sensitive data from interception during transfer

### Technique Summary

- Concealing account numbers
	- Data masking
	- Maintains partial visibility for users

- Secure storage of transaction records
	- Data obfuscation
	- Prevents unauthorized interpretation

- Secure transmission of transaction records
	- Data obfuscation
	- Protects data during transfer

---
## Example 4: Learning Management System (LMS)

### Scenario Overview

- LMS manages student grades, instructor logs, and personal student data
- Data is accessed by users with varying authorisation levels

### Requirement Analysis

- Hiding student grades from unauthorised users
	- Technique: Data masking (dynamic masking)
	- Reason:
		- Restricts full visibility to authorised roles only

- Anonymising instructor access logs for reporting
	- Technique: Data masking (static masking)
	- Reason:
		- Replaces identifiers with pseudonyms
		- Preserves reporting value while protecting privacy

- Securing personal student information during transmission
	- Technique: Data obfuscation (e.g., scrambling)
	- Reason:
		- Prevents interception and misuse of sensitive personal data

### Technique Summary

- Hiding student grades
	- Data masking
	- Prevents unauthorised disclosure

- Anonymising access logs
	- Data masking
	- Protects identities while maintaining usability

- Securing personal data in transit
	- Data obfuscation
	- Ensures unreadable data during transmission

---
## Key Takeaways

- Data masking is best suited for:
	- Partial visibility requirements
	- User interfaces, reporting, and testing
	- Preserving usability with reduced sensitivity

- Data obfuscation is best suited for:
	- Secure storage and transmission
	- Preventing interpretation by unauthorized users
	- High-risk or high-value data protection

- Selecting the correct technique depends on:
	- How the data is used
	- Who needs access
	- The threat model and exposure surface