#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "LinkedListString.h"
//#include "LinkedListString.c"

int main()
{
        /* Start with the empty list */
        struct node* head = NULL;

        push(&head, "Tom");
        push(&head, "Dick");
        push(&head, "Harry");
        push(&head, "Paul");
        push(&head, "Chris");
        push(&head, "Stuart");
        push(&head, "Rob");
        push(&head, "Harry");
        push(&head, "Harry");

        printf("Linked list after insertion: \n");
        printList(head);
        printf("\n");

     /* Step 1: count the number of elements in the list */
        printf("Number of elements in the list: %d\n", listCount(head));
        printf("\n");

     /* Step 2:  delete a particular element/ multiple occurrences of an element from the list */
        deleteElement(&head, "Harry");
        printf("Linked list after deletion: \n");
        printList(head);
        printf("\n");
        printf("Number of elements in the list after deletion: %d\n", listCount(head));
        printf("\n");

     /* Step 3: reverse the elements in a list */
        reverseList(&head);
        printf("Reversed Linked list \n");
        printList(head);
        printf("\n");
        

     /* Step 4: delete all the elements in a linked list */
        
        listAllDelete(&head);
        printf("Number of elements in the list after deletion: %d\n", listCount(head));
        printf("\n");

        return 0;
}