#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void sortString(char *s[], int count);

int main(){

        int i;
        char buff[BUFSIZ];

        int count;
       // 's' is a pointer to a char pointer, initially 's' is allocated storage for one char pointer
        char** s= malloc(sizeof(char*));
        
        printf("Here is the list of unsorted names: \n\n");

        // Keep on reading unlimited number of names until EOF (Ctrl + D) is reached
        for (count = 0; fgets(buff, sizeof(buff), stdin); count++){

			//Step 1: allocate sufficient storage for s[n] to store the content in buff plus one byte for '\0' or replace '\n' with '\0'
			s[count] = malloc(strlen(buff) + 1);//+1 for '\0'

			//Step 2: copy the contents of 'buff' to 's[n]'
			strcpy(s[count]+'\0', buff);

			//Step 3: resize the array of pointers pointed to by 's' to increase its size for the next pointer
			//s = realloc(s, sizeof(s) + sizeof(char*));
			//if above doesn't work: s = realloc(s, sizeof(char*)*(count+2));
			s = realloc(s, sizeof(char*)*(count + 2));
			
        }

       // EOF reached. Now count the number of strings read

        printf("\nCount is %d\n\n", count);

       // Now sort string using sortString function

       // Step 4: implement sortString function for the above-mentioned function declaration
       // sortString(s, count);

		sortString(s, count);

       // Step 5: print the list of sorted names.

		for (int a = 0; a < count; a++) {
			printf("%s", s[a]);//no \n needed since the String contains it
		}
 
       // Step 6:free the allocated memory.
	   
	   for (int a = 0; a < count; a++) {
			free(s[a]);
	   }
	   free(s);
	   
       return 0;
}

void sortString(char *s[], int count){

	int i, j;
	char* temp;

	for (i = 0; i < count - 1; i++) {
		for (j = 0; j < count - 1; j++) {
			if (strcmp(s[j], s[j+1]) > 0) {
				temp = s[j];
				s[j] = s[j + 1];
				s[j + 1] = temp;
			}
		}
	}

}

