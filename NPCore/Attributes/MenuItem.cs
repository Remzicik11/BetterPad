using System;
using NPCore.Internal;
using System.Reflection;
using NPCore.DataTemplates;
using System.Collections.Generic;
using System.Text;

namespace NPCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MenuItem : Attribute
    {
        public Type TargetType;

        public MenuItem(Type TargetType)
        {
            this.TargetType = TargetType;
        }
    }

    public interface IMenuItem
    {
        public void ImportFields(object Target, bool IncludeId)
        {
            var TargetType = Target.GetType();

            var InstanceFields = GetType().GetFields();
            
            for (int i = 0; i < InstanceFields.Length; i++)
            {
                var Field = TargetType.GetField(InstanceFields[i].Name);

                if (Field == null)
                {
                    if (InstanceFields[i].GetCustomAttribute<Optional>() != null) { continue; }
                    throw new NullReferenceException("Targetted type isn't inluded in Instance. Type: [" + TargetType + "::" + InstanceFields[i].Name + "]");
                }
                InstanceFields[i].SetValue(this, Field.GetValue(Target));
            }


        }
    }
}
