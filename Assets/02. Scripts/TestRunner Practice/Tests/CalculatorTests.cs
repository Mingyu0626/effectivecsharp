using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class CalculatorTests
{
    public GameObject _gameObject;
    public Calculator _calculator;

    // 매 테스트 실행 전에 호출, 즉 테스트할 메서드가 100개라면, 총 100회 호출된다.
    [SetUp]
    public void Setup()
    {
        _gameObject = new GameObject("TestObject");
        _calculator = new Calculator();
    }

    // 전체 테스트 수행 전에 단 1회만 실행, 씬 로딩, DB 데이터 접근 등의 무거운 작업은 여기서 수행하는게 좋지만,
    // 여기서 생성한 객체나 데
    // 이터는 모든 테스트가 공유하므로, 테스트 오염의 리스크가 존재한다.
    [OneTimeSetUp] 
    public void OneTimeSetup()
    {

    }

    // 매 테스트 종료시 호출. 즉 테스트할 메서드가 100개라면, 총 100회 호출된다.
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    // 전체 테스트 수행 후 단 1회만 실행. OneTimeSetUp에서 할당한 객체는 여기서 해제해줘야 한다.
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {

    }


    // Unity 생명주기에 연동될 필요가 없는 일반적인 테스트의 경우 Test 어트리뷰트 사용,
    // 반대로 생명주기에 연동되어야 한다면 UnityTest 어트리뷰트 사용
    [Test] // [UnityTest]
    public void GameObject_Exists()
    {
    }

    // 동일한 메서드에 대해 다른 입력값으로 여러번 테스트해보고 싶을 때 사용.
    [TestCase(5, 5, 10)]
    [TestCase(8, 4, 12)]
    public void Add(int a, int b, int expected)
    {
        Assert.AreEqual(a + b, expected);
    }

    // 정적 배열 대신 '메서드'로 데이터를 공급.
    static IEnumerable<TestCaseData> AddDatas()
    {
        // 입력(2,3) + 기대값(Returns 5) + 로그에 찍힐 이름(SetName)
        yield return new TestCaseData(2, 3).Returns(5).SetName("일반 덧셈");
        yield return new TestCaseData(0, 0).Returns(0).SetName("0 더하기");
        yield return new TestCaseData(1, 2).Returns('3').SetName("리턴 타입 불일치 테스트");
    }

    [Test]
    [TestCaseSource(nameof(AddDatas))]
    public int Add_Test_Pro(int a, int b)
    {
        return a + b;
    }


    [TestCase(5, 5, 10)]
    [TestCase(8, 4, 12)]
    [TestCase(0, 0, 1)]
    public void ProductionCirculatorAdd(int a, int b, int expected)
    {
        Assert.AreEqual(_calculator.Add(a, b), expected);
    }

    [TestCase(100, -78, 178)]
    [TestCase(-20, 4, -24)]
    [TestCase(0, 0, -1)]
    public void ProductionCirculatorSubtract(int a, int b, int expected)
    {
        Assert.AreEqual(_calculator.Subtract(a, b), expected);
    }

    [TestCase(5, 5, 25.0)]
    [TestCase(5, 5, 25)]
    [TestCase(-2, 3, -6.0)]
    [TestCase(-70, -80, 5600.0)]
    [TestCase(50000000, 50000000, 2500000000000000.0)]
    public void ProductionCirculatorMultiplication(int a, int b, double expected)
    {
        Assert.AreEqual(_calculator.Multiplication(a, b), expected);
    }

    [TestCase(100, 20, 5.0)]
    [TestCase(100, 20, 5)]
    [TestCase(0, 3, 0.0)]
    [TestCase(7, 0, 0.0)]
    [TestCase(7, 0, null)]
    [TestCase(0, 7, 0.0)]
    [TestCase(5, 2, 2.5)]
    public void ProductionCirculatorDivision(int dividend, int divisor, double? expected)
    {
        if (divisor == 0)
        {
            LogAssert.Expect(LogType.Exception, "DivideByZeroException: Attempted to divide by zero.");
        }
        Assert.AreEqual(_calculator.Division(dividend, divisor), expected);
    }
}

