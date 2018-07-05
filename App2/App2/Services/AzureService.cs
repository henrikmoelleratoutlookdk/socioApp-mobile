using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

using App2.Models;
using Plugin.Connectivity;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace App2.Services
{
    public class AzureService : IService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Assignment> AssignmentTable;

        bool isInitialized;

        public async Task Initialize()
        {
            if (isInitialized)
                return;

            //TODO 1: Create our client
            //Create our client
            MobileService = new MobileServiceClient(Helpers.Keys.AzureServiceUrl, null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            //TODO 2: Create our database store & define a table.
            var store = new MobileServiceSQLiteStore("Assignment.db");
            store.DefineTable<Assignment>();

            //MobileServiceSyncHandler - Handles table operation errors and push completion results.
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            AssignmentTable = MobileService.GetSyncTable<Assignment>();

            isInitialized = true;
        }

        public async Task SyncAssignments()
        {
            //TODO 3: Add connectivity check. 
            var connected = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable(Helpers.Keys.AzureServiceUrl);
            if (connected == false)
                return; 

            try
            {
                //TODO 4: Push and Pull our data
                await MobileService.SyncContext.PushAsync();
                await AssignmentTable.PullAsync("allSyncAssignments", AssignmentTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync Assignments, that is alright as we have offline capabilities: " + ex);
            }
        }

        public async Task<IEnumerable<Assignment>> GetAssignments()
        {
            await Initialize();
            await SyncAssignments();
            return await AssignmentTable.ToEnumerableAsync();
        }

        public async Task<Assignment> AddAssignment(string text, bool assignmentActive)
        {
            await Initialize();
            var item = new Assignment
            {
                Text = text,
                AssignmentOk = assignmentActive
            };

            //TODO 5: Insert item into todoTable
            await AssignmentTable.InsertAsync(item);
            //Synchronize todos
            await SyncAssignments();
            return item;
        }

        public async Task<Assignment> UpdateAssignment(Assignment item)
        {
            await Initialize();

            //TODO 6: Update item
            await AssignmentTable.UpdateAsync(item);

            //Synchronize todos
            await SyncAssignments();
            return item;
        }

        public async Task<bool> DeleteAssignment(Assignment item)
        {
            await Initialize();
            try
            {
                //TODO 7: Delete item and Sync
                await AssignmentTable.DeleteAsync(item);
                await SyncAssignments();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}