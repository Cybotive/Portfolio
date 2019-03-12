#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "LinkedListString.h"

/* Link list node */
struct node
{
        char data[40];
        struct node* next;
};

/* Function to push a node */
void push(struct node** head_ref, char* new_data)
{
        /* allocate node */
        struct node* new_node = malloc(sizeof(struct node));

        /* put in the data  */

        strcpy(new_node->data, new_data);


        /* link the old list off the new node */
        new_node->next = (*head_ref);

        /* move the head to point to the new node */
        (*head_ref) = new_node;
}

/* Function to print linked list */
void printList(struct node *head)
{
        struct node *temp = head;
        while (temp != NULL)
        {
                printf("%s  ", temp->data);
                temp = temp->next;
        }
}

/* Function to count the number of elements in the linked list */
int listCount(struct node *head){
    int count = 0;

    struct node *curr = head;
    
    for(count = 0; curr != NULL; count++, curr = curr->next);//traverse and increment

    return count;
}

/*Function to delete a particular element in a linked list*/
void deleteElement(struct node **currP, char *value){
    struct node *curr = *currP;
    struct node *prev = NULL;

    while(curr != NULL){
        if(strcmp(curr->data, value) == 0){//if curr data is same as target
            if(prev != NULL){
                prev->next = curr->next;
                free(curr);
            }
            else{
                prev = curr;
                curr = curr->next;
                free(prev);
                prev = NULL;
                *currP = curr;
            }
        }
        else{
            prev = curr;
            curr = curr->next;
        }
        
    }

}

/* Function to reverse the linked list */
void reverseList(struct node** head_ref){
    struct node *head = *head_ref;
    struct node *curr = head;

    /*struct node *revHead = head;
    while(revHead->next != NULL){
        revHead = revHead->next;
    }*/

    //printf("\n%s\n", revHead->data);

    struct node* newHead = NULL;

    int size = listCount(head);
    for(int i = 0; i < size; i++){ //this might seem taxing, but its more efficient than reassigning next pointers
        push(&newHead, curr->data);
        struct node *del = curr;
        curr = curr->next;
        free(del);
    }

    *head_ref = newHead;

}

/*Function to delete all elements in a Linked List */
void listAllDelete(struct node **currP){
    struct node *curr = *currP;

    while(curr != NULL){
        struct node *del = curr;
        curr = curr->next;
        free(del);
        *currP = curr;
    }

}