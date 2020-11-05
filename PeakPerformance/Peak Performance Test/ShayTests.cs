using System;
using NUnit.Framework;
using Peak_Performance.Controllers;
using Peak_Performance.Models;
using System.Collections.Generic;
using Peak_Performance.Models.ViewModels;

namespace Peak_Performance_Test
{
    [TestFixture]
    public class ShayTests
    {

        [Test]
        public void CountExercisesInViewModel()
        {
           /* WorkoutsViewModel workoutsViewModel = new WorkoutsViewModel();
            workoutsViewModel.team = "Wildcats";
            workoutsViewModel.date = DateTime.Now;

            ComplexesViewModel complexesViewModel = new ComplexesViewModel();
            List<ComplexViewModel> complexes = new List<ComplexViewModel>();
            ComplexViewModel c1 = new ComplexViewModel();
            ComplexViewModel c2 = new ComplexViewModel();
            List<ExerciseViewModel> exercises1 = new List<ExerciseViewModel>();
            List<ExerciseViewModel> exercises2 = new List<ExerciseViewModel>();

            ExerciseViewModel e1 = new ExerciseViewModel("squats", 3, 10, 125, null, null, null);
            ExerciseViewModel e2 = new ExerciseViewModel("lunges", 3, 10, 100, null, null, null);
            ExerciseViewModel e3 = new ExerciseViewModel("bench press", 3, 10, 80, null, null, null);

            exercises1.Add(e1);
            exercises1.Add(e2);
            exercises1.Add(e3);

            c1.exercise = exercises1;

            complexes.Add(c1);

            ExerciseViewModel e4 = new ExerciseViewModel("sprints", 3, 5, null, null, null, 100);
            ExerciseViewModel e5 = new ExerciseViewModel("jog", 1, 1, null, null, null, 1500);

            exercises2.Add(e4);
            exercises2.Add(e5);

            c2.exercise = exercises2;

            complexes.Add(c2);

            complexesViewModel.complex = complexes;

            workoutsViewModel.complexes = complexesViewModel;

            List<int> testvalue = workoutsViewModel.CountExercises();

            List<int> expected = new List<int> { 3, 2 };

            Assert.That(testvalue, Is.EqualTo(expected));*/
        }

        //[Test]
        //public void CountExercisesInViewModel()
        //{
        //    WorkoutsViewModel workoutsViewModel = new WorkoutsViewModel();
        //    workoutsViewModel.team = "Wildcats";
        //    workoutsViewModel.date = DateTime.Now;

        //    ComplexesViewModel complexesViewModel = new ComplexesViewModel();
        //    List<ComplexViewModel> complexes = new List<ComplexViewModel>();
        //    ComplexViewModel c1 = new ComplexViewModel();
        //    ComplexViewModel c2 = new ComplexViewModel();
        //    List<ExerciseViewModel> exercises1 = new List<ExerciseViewModel>();
        //    List<ExerciseViewModel> exercises2 = new List<ExerciseViewModel>();

        //    ExerciseViewModel e1 = new ExerciseViewModel("squats", 3, 10, 125, null, null, null);
        //    ExerciseViewModel e2 = new ExerciseViewModel("lunges", 3, 10, 100, null, null, null);
        //    ExerciseViewModel e3 = new ExerciseViewModel("bench press", 3, 10, 80, null, null, null);

        //    exercises1.Add(e1);
        //    exercises1.Add(e2);
        //    exercises1.Add(e3);

        //    c1.exercise = exercises1;

        //    complexes.Add(c1);

        //    ExerciseViewModel e4 = new ExerciseViewModel("sprints", 3, 5, null, null, null, 100);
        //    ExerciseViewModel e5 = new ExerciseViewModel("jog", 1, 1, null, null, null, 1500);

        //    exercises2.Add(e4);
        //    exercises2.Add(e5);

        //    c2.exercise = exercises2;

        //    complexes.Add(c2);

        //    complexesViewModel.complex = complexes;

        //    workoutsViewModel.complexes = complexesViewModel;

        //    List<int> testvalue = workoutsViewModel.CountExercises();

        //    List<int> expected = new List<int> { 3, 2 };

        //    Assert.That(testvalue, Is.EqualTo(expected));
        //}

    }
}