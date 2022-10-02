using JurmasModels;
using JurmasWPF.Commands;
using JurmasWPF.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JurmasWPF.ViewModels
{
    internal class CreateRecipeeViewModel : ViewModelBase
    {
		public delegate void CancelCloseWindowDelegate();
		public event CancelCloseWindowDelegate CancelCloseWindowEvent;

		public delegate void RecipeeCreatedDelegate(Recipee recipee);
		public event RecipeeCreatedDelegate RecipeeCreatedEvent;

		private string title = String.Empty;
		public string RecipeeTitle
		{
			get { return title; }
			set { title = value; NotifyPropertyChanged(); }
		}

		private string description = String.Empty;
		public string RecipeeDescription
		{
			get { return description; }
			set { description = value; NotifyPropertyChanged(); }
		}

		private FakeData repo;

		private Recipee Recipee { get; set; }

		public ICommand SaveRecipeeCommand { get; }
		public ICommand CancelCommand { get; }

		public CreateRecipeeViewModel()
		{
			this.repo = App.AppHost.Services.GetService<FakeData>();
            this.Recipee = new Recipee();

            this.SaveRecipeeCommand = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSave);
			this.CancelCommand = new ViewModelCommand(ExecuteCancelCommand);
		}

		private void ExecuteCancelCommand(object obj)
		{
			CancelCloseWindowEvent.Invoke();
		}

		private void ExecuteSaveCommand(object obj)
		{
			this.Recipee.Title = this.RecipeeTitle;
			this.Recipee.Description = this.RecipeeDescription;
			this.Recipee.DateCreated = DateTime.Now;
			this.Recipee.DateUpdated = DateTime.Now;
			this.Recipee.CreatedBy = 1;

			this.repo.AddRecipee(this.Recipee);

			CancelCloseWindowEvent.Invoke();
		}

		private bool CanExecuteSave(object obj)
		{
			if (string.IsNullOrEmpty(this.RecipeeTitle)
				&& string.IsNullOrEmpty(this.RecipeeDescription))
			{
				return false;
			}

			return true;
		}

		internal void SetRecipee(Recipee recipee)
		{
			this.Recipee = recipee;
			this.RecipeeTitle = this.Recipee.Title;
			this.RecipeeDescription = this.Recipee.Description;
		}
	}
}
