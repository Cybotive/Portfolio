/* Link list node */
struct node;

/* Function to push a node */
void push(struct node** head_ref, char* new_data);

/* Function to print linked list */
void printList(struct node *head);

/* Function to count the number of elements in the linked list */
int listCount(struct node *head);

/* Function to reverse the linked list */
void reverseList(struct node** head_ref);

/*Function to delete all elements in a Linked List */
void listAllDelete(struct node **currP);

/*Function to delete a particular element in a linked list*/
void deleteElement(struct node **currP, char *value);