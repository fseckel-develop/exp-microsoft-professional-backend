## Example 1: Creating the Basic Registration Form

```html
<form id="registrationForm">
    <label for="name">Name:</label>
    <input type="text" id="name" name="name" required><br><br>
    <label for="email">Email:</label>
    <input type="email" id="email" name="email" required><br><br>
    <label for="password">Password:</label>
    <input type="password" id="password" name="password" required minlength="6"><br><br>
    <button type="submit">Register</button>
</form>
```

1. Name Field:
    - The required attribute ensures the user cannot leave the field blank.

2. Email Field:
    - The type="email" attribute ensures the input follows a valid email format, 
      such as containing @ and a domain.

3. Password Field:
    - The type="password" masks the input for privacy.
    - The minlength="6" attribute ensures the password is at least 6 characters long.

4. Submit Button:
    - Triggers form submission when clicked, provided all fields pass validation.

---
## Example 2: Expanding the Form with New Fields

```html
<form id="registrationForm">
    <label for="name">Name:</label>
    <input type="text" id="name" name="name" required><br><br>
    <label for="email">Email:</label>
    <input type="email" id="email" name="email" required><br><br>
    <label for="password">Password:</label>
    <input type="password" id="password" name="password" required minlength="6"><br><br>
    <label for="confirmPassword">Confirm Password:</label>
    <input type="password" id="confirmPassword" name="confirmPassword" required 
           pattern=".{6,}" title="Must match the password"><br><br>
    <label for="phone">Phone Number:</label>
    <input type="tel" id="phone" name="phone" required 
           pattern="\d{10}" title="Must be 10 digits"><br><br>
    <button type="submit" id=”btnSubmit”>Register</button>
</form>
```

1. Confirm Password Field:
    - The required attribute ensures this field cannot be left empty.
    - The pattern=".{6,}" enforces a minimum length of 6 characters.
    - The title provides a helpful tooltip when the user hovers over the field or fails validation.

2. Phone Number Field:
    - The type="tel" accepts numeric input.
    - The pattern="\d{10}" ensures exactly 10 numeric digits are entered.
    - The title explains the format requirement to the user.

---
## Task 1: Add a Username Field

**Objective:** Add a Username field with the following validation rules:

1. Alphanumeric characters only.
2. Length between 4 and 12 characters.

**Instructions:**

1. Refer to Examples 1 and 2 for guidance.
2. Add a Username field to the form with:
    - The required attribute to make it mandatory.
    - The pattern="`[a-zA-Z0-9]{4,12}`" attribute to enforce alphanumeric chars and length.
    - The title attribute to provide guidance.

```html
<form id="registrationForm">
    <label for="name">Name:</label>
    <input type="text" id="name" name="name" required><br><br>
    <label for="email">Email:</label>
    <input type="email" id="email" name="email" required><br><br>
    
    <label for="username">Username:</label>
    <input type="text" id="username" name="username" required 
           pattern="[a-zA-Z0-9]{4,12}" 
           title="Must be 4-12 characters long and alphanumeric"><br><br>
           
    <label for="password">Password:</label>
    <input type="password" id="password" name="password" required minlength="6"><br><br>
    <label for="confirmPassword">Confirm Password:</label>
    <input type="password" id="confirmPassword" name="confirmPassword" required 
           pattern=".{6,}" title="Must match the password"><br><br>
    <label for="phone">Phone Number:</label>
    <input type="tel" id="phone" name="phone" required 
           pattern="\d{10}" title="Must be 10 digits"><br><br>
    <button type="submit" id=”btnSubmit”>Register</button>
</form>
```

1. The **Username** field uses pattern to allow only alphanumeric characters and enforce a length between 4 and 12 characters.
2. The title provides feedback to users about the requirements.

---
## Task 2: Add Password Confirmation

**Objective:** Enhance the registration form by adding:

1. Confirm Password: Ensures the input matches the password field.

**Instructions:**

1. Refer to Example 2.
2. Add a Confirm Password field with:
    - The required attribute to make it mandatory.
    - The pattern attribute to enforce a minimum length.
    - The title attribute to explain the requirement.
3. Add JavaScript to the page to compare the value of the “password” field and the “confirmPassword” field.

```html
<form id="registrationForm">
    <label for="name">Name:</label>
    <input type="text" id="name" name="name" required><br><br>
    <label for="email">Email:</label>
    <input type="email" id="email" name="email" required><br><br>
    
    <label for="username">Username:</label>
    <input type="text" id="username" name="username" required 
           pattern="[a-zA-Z0-9]{4,12}" 
           title="Must be 4-12 characters long and alphanumeric"><br><br>
    
    <label for="password">Password:</label>
    <input type="password" id="password" name="password" required minlength="6"><br><br>
    <label for="confirmPassword">Confirm Password:</label>
    <input type="password" id="confirmPassword" name="confirmPassword" required 
           pattern=".{6,}" title="Must match the password"><br><br>
    <label for="phone">Phone Number:</label>
    <input type="tel" id="phone" name="phone" required 
           pattern="\d{10}" title="Must be 10 digits"><br><br>
    <button type="submit" id=”btnSubmit”>Register</button>
</form>

<script>
document.getElementById('registrationForm')
	.addEventListener('submit', function(event) 
{
	event.preventDefault(); // this prevents normal form submission
	
	const password = document.getElementById('password').value;
	const confirmPassword = document.getElementById('confirmPassword').value;
	if (password !== confirmPassword) { 
		document.getElementById('confirmPassword')
			.setCustomValidity("Password doesn't match");
		return false;
	} 
	else { 
		document.getElementById('confirmPassword')
			.setCustomValidity("");
		return true;
	}
});
</script>
```

1. The **Confirm Password** field matches the password’s minimum length requirement 
   using pattern and ensures input is required.
2. The JavaScript executes when the form is submitted. 
   It then compares the value of the “password” and “confirmPassword” fields. 
   If they don’t match then it will display a message by the “confirmPassword” field 
   and it will not submit the form.