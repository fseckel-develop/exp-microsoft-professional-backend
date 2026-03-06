## Example 1: Refactoring a Simple Loop

### Original Code

```javascript
const data = [1, 2, 3, 4, 5];
let result = [];

for (let i = 0; i < data.length; i++) {
  if (data[i] % 2 === 0) {
    result.push(data[i] * 2);
  }
}

console.log(result);  // Expected output: [4, 8]
````

### Sample Copilot Output

```javascript
const result = data
  .filter(num => num % 2 === 0)
  .map(num => num * 2);

console.log(result);  // Output: [4, 8]
```

### Step-by-Step Evaluation

#### Step 1: Efficiency

- Reduces multiple lines of loop logic into a concise functional chain.
- Uses optimised built-in array methods.

#### Step 2: Readability

- Clear separation of filtering and transformation logic.
- Easier to understand for developers familiar with functional programming.

#### Step 3: Maintainability

- Less repetitive structure.
- Lower risk of introducing loop-related errors during updates.

### Decision: Accept

- Reason:
    - The refactored code is shorter, clearer, and functionally equivalent.
    - Improves maintainability without changing behaviour.

---
## Example 2: Refactoring Repeated Database Queries

### Original Code

```javascript
const getData = async (id) => {
  const data = await db.query(`SELECT * FROM users WHERE id = ${id}`);
  return data;
};

const user1 = await getData(1);
const user2 = await getData(2);
```

### Sample Copilot Output

```javascript
const getUserData = async (ids) => {
  return Promise.all(
    ids.map(id => db.query(`SELECT * FROM users WHERE id = ${id}`))
  );
};

const [user1, user2] = await getUserData([1, 2]);
```

### Step-by-Step Evaluation

#### Step 1: Efficiency

- Uses Promise.all to process queries in parallel.
- Reduces sequential waiting time between calls.

#### Step 2: Readability

- Eliminates repetitive function calls.
- Scales easily for additional user IDs.

#### Step 3: Maintainability

- Encapsulates logic in a reusable function.
- Better suited for scaling and extension.

### Decision: Accept

- Reason:
    - Improves performance through parallel execution.
    - Enhances scalability and reduces redundancy.

---
## Example 3: Refactoring Error Handling

### Original Code

```javascript
try {
  const data = await db.query("SELECT * FROM users WHERE active = true");
  console.log(data);
} catch (error) {
  console.log("Error fetching data");
}
```

### Sample Copilot Output

```javascript
try {
  const data = await db.query("SELECT * FROM users WHERE active = true");
  console.log(data);
} catch (error) {
  console.error("An error occurred:", error);
}
```

### Step-by-Step Evaluation

#### Step 1: Efficiency

- No performance impact.
- Focuses on improving debugging clarity.

#### Step 2: Readability

- Clearly distinguishes errors from normal log output.
- Displays the actual error object for deeper inspection.  

#### Step 3: Maintainability

- Provides better visibility into failures.
- Simplifies troubleshooting in development and production logs.    

### Decision: Accept

- Reason:
    - Improves debugging capability.
    - Retains original functionality while adding valuable diagnostic detail.

---
## Example 3: Refactoring Functions

### Original Code

```javascript
function calculateArea(width, height) {
  return width * height;
}

function calculatePerimeter(width, height) {
  return 2 * (width + height);
}
```

### Sample Copilot Output

```javascript
const calculateRectangle = {
  area: (width, height) => width * height,
  perimeter: (width, height) => 2 * (width + height)
};
```

### Step-by-Step Evaluation

#### Step 1: Efficiency

- No significant performance difference.
- Focuses on structural organisation.

#### Step 2: Readability

- Groups related functionality together.
- Makes the relationship between area and perimeter explicit.

#### Step 3: Maintainability

- Reduces global namespace clutter.
- Simplifies extension (e.g., adding new rectangle-related methods).

### Decision: Accept

- Reason:
    - Improves logical grouping and organisation.
    - Enhances clarity without altering behaviour.

---