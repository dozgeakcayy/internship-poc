# Clean Code

## What is Clean Code?

Clean Code is code that is easy to read, understand, maintain, and improve. It helps developers work more efficiently and reduces the possibility of bugs.

## Why is Clean Code Important?

- Improves readability.
- Makes maintenance easier.
- Reduces bugs.
- Increases teamwork efficiency.
- Makes future development easier.

## 7 Basic Principles of Clean Code

1. Use meaningful names.
2. Keep functions short and focused.
3. Avoid unnecessary comments.
4. Each function should have a single responsibility.
5. Don't Repeat Yourself (DRY).
6. Write properly formatted code.
7. Remove unused code.

---

# SOLID Principles

## S - Single Responsibility Principle
A class should have only one responsibility.

## O - Open/Closed Principle
Software should be open for extension but closed for modification.

## L - Liskov Substitution Principle
Derived classes should be replaceable with their base classes without changing the correctness of the program.

## I - Interface Segregation Principle
Classes should not be forced to implement methods they do not use.

## D - Dependency Inversion Principle
High-level modules should depend on abstractions instead of concrete implementations.

---

# Good Code vs Bad Code

## Bad Code

```csharp
int x = 18;
int y = 20;

if(y >= x)
{
    Console.WriteLine("OK");
}
```

Problems:
- Variable names are unclear.
- Code is difficult to understand.

## Good Code

```csharp
int minimumAge = 18;
int userAge = 20;

if(userAge >= minimumAge)
{
    Console.WriteLine("User can register.");
}
```

Advantages:
- Meaningful variable names.
- Easy to read.
- Easy to maintain.
