# Loop Performance Test

Today our customer report a good point to improve the performance in our product. It is about the loop in .NET. In the test, they found the `for` loop is has better performance than `foreach`. 
I know this one for a long time. But when I verified it, I found it doesn't affect the performance too much, but really affects by the type of list you are using. See the result below.

**Loop with List<T>**

![preview-list](https://github.com/howardchn/LoopPerformanceTest/blob/master/preview-list.png?raw=true)

**Loop with Array**

![preview-array](https://github.com/howardchn/LoopPerformanceTest/blob/master/preview-array.png?raw=true)

**Loop with ArrayList**

![preview-arraylist](https://github.com/howardchn/LoopPerformanceTest/blob/master/preview-arraylist.png?raw=true)


**Loop with Collection<T>**

![preview-collection](https://github.com/howardchn/LoopPerformanceTest/blob/master/preview-collection.png?raw=true)

If I didn't do this test, I cannot tell which is better. But next time, I guess I need to be more careful to use the list type from now on.