def factorial(number):
    result = 1
    for i in xrange(2, number + 1):
        result *= i
    return result