# 反射(Reflection)對物件

#### 取得 Object Type
```c#
var type = Obj.GetType();
or
var type = typeof(T);
```

#### 取得 Object Property Value
```c#
Type srcObjType = Obj.GetType();
var value = srcObjType.GetProperty(propertyInfo.Name)?.GetValue(Obj, null);
```

#### 設置 Object Property Value
```c#
propertyInfo.SetValue(resultObj, value, null);
```

#### 取得 List element objtect Type
```c#
var genericType = property.PropertyType.GetGenericArguments().First();
```
#### 建立 List object
```c#
Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
```

### Object屬性對照表

|    類別\屬性        |    IsArray    |    IsClass    |     IsEnum    | IsGenericType |  IsValueType  |
| -------------      | ------------- | ------------- | ------------- | ------------- | ------------- |
| int                |               |               |               |               |       V       |
| int?               |               |               |               |      V        |       V       |
| long               |               |               |               |               |       V       |
| string             |               |       V       |               |               |       V       |
| string[]           |       V       |       V       |               |               |               |
| enum               |               |               |       V       |               |       V       |
| List<T>            |               |       V       |               |      V        |               |
| Dictionary<T1,T2>  |               |       V       |               |      V        |               |
| Class              |               |       V       |               |               |               |