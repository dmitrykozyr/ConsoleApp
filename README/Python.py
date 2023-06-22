# Классы, имя писать с большой буквы
class Point:
    color = 'red'
    circle = 2

    # Если не передаем в ф-ю аргументы, пишем self
    def SetValues(self, x, y):
        self._x = x
        self._y = y
        print(x, y)

    def GetValues(self):
        # Возвращаем кортеж
        return (self._x, self._y)

#print(Point.color)
Point.color = 'white'
#print(Point.__dict__) # Вернуть все аттрибуты класса

objPoint = Point()
print(objPoint.circle)
objPoint.SetValues(1, 2)
print(objPoint.GetValues())

