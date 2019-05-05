using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestJob.Model;

namespace TestJobTest.Commands
{
    [TestClass]
    public class AttrubutesComandCheck
    {
        static Type[] GetTypesOfAssembly(System.Reflection.Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (System.Reflection.ReflectionTypeLoadException refl)
            {
                return refl.Types;
            }
        }

        /// <summary>
        /// Проверяем что бы у каждой команды было описание 
        /// </summary>
        [TestMethod]
        public void CheckDescriptionIsExist() {

        }
    }  
}
