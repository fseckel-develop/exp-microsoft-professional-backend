## Objective

Errors can prevent applications from running correctly or cause unexpected crashes. 
Using Microsoft Copilot, developers can quickly identify, diagnose, and fix common coding errors such as:

- Null reference errors  
- Logic flaws  
- Divide-by-zero exceptions  
- File handling errors  

**Goal:** Use Copilot to detect issues, apply suggested fixes, and verify improved app stability.

---
## Example 1: Fixing a Null Reference Error in C#

### Scenario

A developer writes the following C# code but encounters a `NullReferenceException` when running it:

```csharp
string message = null;

Console.WriteLine(message.Length);  // This will trigger a NullReferenceException
```

### Step-by-Step Debugging Process with Copilot

#### Error Detection

**Prompt:**  "What’s wrong with this code?"

**Copilot Response:** "This code attempts to access the Length property of a null string, causing 
a NullReferenceException. To prevent this, add a null check before accessing the property."

#### Suggested Fix

```csharp
string message = null;

if (message != null)
{
    Console.WriteLine(message.Length);
}
else
{
    Console.WriteLine("No message available.");
}
```

#### Verification and Testing

**Prompt:** "How do I confirm this fix works?"

**Simulated Copilot Response:** "Run the modified code with different values for `message` 
(null and non-null) to ensure it no longer crashes and displays the expected output."

**Result:**
- Prevents runtime crash  
- Handles null safely  
- Improves program stability  

---
## Example 2: Fixing a Logic Flaw in a Discount Calculation

### Scenario

A developer writes a function that incorrectly applies a 10% discount instead of 20% for prices above $100.

### Step-by-Step Debugging Process with Copilot

#### Error Detection

**Prompt:** "This discount logic is wrong. How can I fix it?"

**Simulated Copilot Response:** "The function applies a 10% discount instead of the intended 20% for prices over $100. Modify the calculation to apply a 20% discount."

#### Suggested Fix

```csharp
static decimal ApplyDiscount(decimal price)
{
    if (price > 100)
    {
        return price * 0.20m;  // Correct logic
    }
    return 0;
}

decimal price = 150m;
decimal discount = ApplyDiscount(price);
Console.WriteLine($"Price: {price}, Discounted Price: {price - discount}");
```

#### Verification and Testing

**Prompt:** "How do I ensure this fix is correct?"

**Simulated Copilot Response:** "Test the function with multiple price values (below and 
above $100) to confirm that a 20% discount is applied correctly only when appropriate."

**Result:**
- Correct discount logic implemented  
- Output matches business requirements  
- Improved functional accuracy  

---
## Example 3: Fix a Divide-By-Zero Error

### Scenario

The following code crashes when the divisor is zero:

```csharp
int dividend = 10;
int divisor = 0;

int result = dividend / divisor;  // This will throw a DivideByZeroException
Console.WriteLine($"Result: {result}");
```

### Step-by-Step Debugging Process with Copilot

#### Error Detection

**Prompt:** "Why does this code crash?"

**Expected Copilot Response:** "The divisor is zero, causing a DivideByZeroException. 
Add a conditional check before performing division."

#### Implement Copilot’s Suggested Fix

```csharp
int dividend = 10;
int divisor = 0;

if (divisor != 0)
{
    int result = dividend / divisor;
    Console.WriteLine($"Result: {result}");
}
else
{
    Console.WriteLine("Division by zero is not allowed.");
}
```

#### How the Fix Improves Stability

- Prevents `DivideByZeroException`
- Ensures safe mathematical operations
- Provides clear user feedback instead of crashing
- Makes the program more robust in edge cases

---
## Example 4: Fix a File Handling Error

### Scenario

The following code may crash if the file does not exist:

```csharp
string content = File.ReadAllText("data.txt");

Console.WriteLine(content);
```

### Step-by-Step Debugging Process with Copilot

#### Error Detection

**Prompt:** "What potential issue exists in this file-reading code?"

**Expected Copilot Response:** "If the file does not exist, File.ReadAllText will throw a FileNotFoundException. Add error handling."

#### Suggested Fix (Option 1 – Try-Catch)

```csharp
try
{
    string content = File.ReadAllText("data.txt");
    Console.WriteLine(content);
}
catch (Exception ex)
{
    Console.WriteLine($"File not found: {ex.Message}");
}
```

#### Alternative Fix (Using File.Exists)

```csharp
if (File.Exists("data.txt"))
{
    string content = File.ReadAllText("data.txt");
    Console.WriteLine(content);
}
else
{
    Console.WriteLine("File not found.");
}
```

#### Why the Fix Is Necessary

- Prevents application crashes
- Provides meaningful error messages
- Improves user experience
- Handles edge cases gracefully
- `try-catch` is more comprehensive (e.g., handles locked files or access errors)

---