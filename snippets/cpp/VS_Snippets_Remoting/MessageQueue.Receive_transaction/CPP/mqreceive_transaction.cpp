

// <Snippet1>
#using <system.dll>
#using <system.messaging.dll>

using namespace System;
using namespace System::Messaging;

/// <summary>
/// Provides a container class for the example.
/// </summary>
ref class MyNewQueue
{
public:

   //*************************************************
   // Sends a message to a queue.
   //*************************************************
   void SendMessageTransactional()
   {
      // Connect to a queue on the local computer.
      MessageQueue^ myQueue = gcnew MessageQueue( ".\\myTransactionalQueue" );

      // Send a message to the queue.
      if ( myQueue->Transactional )
      {
         // Create a transaction.
         MessageQueueTransaction^ myTransaction = gcnew MessageQueueTransaction;

         // Begin the transaction.
         myTransaction->Begin();

         // Send the message.
         myQueue->Send( "My Message Data.", myTransaction );

         // Commit the transaction.
         myTransaction->Commit();
      }

      return;
   }


   //*************************************************
   // Receives a message containing an Order.
   //*************************************************
   void ReceiveMessageTransactional()
   {
      // Connect to a transactional queue on the local computer.
      MessageQueue^ myQueue = gcnew MessageQueue( ".\\myTransactionalQueue" );

      // Set the formatter.
      array<Type^>^p = gcnew array<Type^>(1);
      p[ 0 ] = String::typeid;
      myQueue->Formatter = gcnew XmlMessageFormatter( p );

      // Create a transaction.
      MessageQueueTransaction^ myTransaction = gcnew MessageQueueTransaction;
      try
      {
         // Begin the transaction.
         myTransaction->Begin();

         // Receive the message. 
         Message^ myMessage = myQueue->Receive( myTransaction );
         String^ myOrder = static_cast<String^>(myMessage->Body);

         // Display message information.
         Console::WriteLine( myOrder );

         // Commit the transaction.
         myTransaction->Commit();
      }
      catch ( MessageQueueException^ e ) 
      {
         // Handle nontransactional queues.
         if ( e->MessageQueueErrorCode == MessageQueueErrorCode::TransactionUsage )
         {
            Console::WriteLine( "Queue is not transactional." );
         }

         // Else catch other sources of a MessageQueueException.
         // Roll back the transaction.
         myTransaction->Abort();
      }

      // Catch other exceptions as necessary, such as 
      // InvalidOperationException, thrown when the formatter 
      // cannot deserialize the message.
      return;
   }
};

//*************************************************
// Provides an entry point into the application.
// 
// This example sends and receives a message from
// a transactional queue.
//*************************************************
int main()
{
   // Create a new instance of the class.
   MyNewQueue^ myNewQueue = gcnew MyNewQueue;

   // Send a message to a queue.
   myNewQueue->SendMessageTransactional();

   // Receive a message from a queue.
   myNewQueue->ReceiveMessageTransactional();
   return 0;
}
// </Snippet1>
