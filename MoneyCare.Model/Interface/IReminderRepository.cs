using System;
using System.Collections.Generic;


namespace MoneyCare.Model
{
    public interface IReminderRepository
    {
        Reminder GetById(Guid id);
        List<Reminder> GetByMonth(int month, int year);
        void Save(Reminder reminder);
        void Update(Reminder reminder);
        void Delete(Guid id);
    }
}
