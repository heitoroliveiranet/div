using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

 public T Execute(string line)
        {
            T concreteObject = (T)Activator.CreateInstance(typeof(T));
            var blocks = line.Split(Separator);

            for (int i = 0; i < blocks.Length; i++)
            {
                var value = blocks[i];
                var itemConfig = Configuration.FirstOrDefault(x => x.ColumnIndex == i);

                if (itemConfig != null)
                { 
                    var expression = itemConfig.Item;
                    var propertyName = GetPropertyName(expression);
                    var propertyInfo = concreteObject.GetType().GetProperty(propertyName);
                    dynamic valueChanged = null;
                    try
                    {
                        valueChanged = Convert.ChangeType(value, propertyInfo.PropertyType);
                    }
                    catch (Exception)
                    {
                    }
                    propertyInfo.SetValue(concreteObject, valueChanged, null);
                }
            }


            return (T)concreteObject;
        }
        private string GetPropertyName(Expression<Func<T, dynamic>> expression)
        {
            var name = "";
            if (expression.Body is MemberExpression)
            {
                name = ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                name = ((MemberExpression)op).Member.Name;
            }
            return name;
        }