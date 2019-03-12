#include <stdio.h>
#include <stdlib.h>
#include <math.h>

void swapElements(int *a, int *b);
void sortArray(int *array, const int size);
void changeElements(int *val);
void printArray(int *array, const int size);
double findMean(int *array, const int size);
double findMedian(int *array, const int size);
double findStandardDeviation( int *array, const int size, double average);
int* allocIntBlock(int numOfInts);

int main(){

	int  n, *x;
        double mean = 0.0, median = 0.0, stdDev = 0.0;

	printf("This is the basic part of the program that asks the user to type the number of integers, i.e., 'n'. Next, allocate memory for 'n' integers, read the values of 'n' integers into the allocated memory usining scanf; next, find the mean, median and average of 'n' integers.Lastly, the allocated memory needs to be freed.\n");
        
	printf("\nRead using scanf how many integers you would like to type:\n");
        scanf("%d", &n);

        
	/*****************************************************************/

	//x is not automatically assigned a memory block when it is defined as a pointer variable, you need to allocate a block
	//of memory large enough to hold 'n' integers
        // Write the function that allocates memory to hold 'n' integers
		
	x = allocIntBlock(n);
	//printf("%lu\n", sizeof(x)); //test

        
    printf("Please type 'n' integers: \n");
	/***********************************************************************/ 
	//Read in the list of numbers 'n' into the allocated block using scanf 
	
	for(int i = 0; i < n; i++){
		scanf("%i", x+i);
	}
	
    printf("Displaying the numbers:\n");

       // Call printArray to display the integers      
	   
	printArray(x, n);
	
	/*//Testing Selection Sort
	sortArray(x, n);
	printArray(x, n);*/
	
        // Find the mean of integers using findMean function
		
	mean = findMean(x, n);
    printf("Mean of the numbers is: %f\n", mean);

        // Fidn the median of integers using findMedian function
		
	median = findMedian(x, n);
    printf("Median of the numbers is: %f\n", median);
	
        // Find the standard deviation of integers using findStandardDeviation function
		
	stdDev = findStandardDeviation(x, n, mean);
    printf("Standard deviation of the numbers is: %f\n", stdDev);
	
       //Deallocate the memory ;
        
    free(x);
        
    return 0;
}


int* allocIntBlock(int n){ //"Write the function that allocates memory to hold 'n' integers"
	return malloc(sizeof(int) * n);
}


void printArray(int *array, const int size){
	
    if(array == NULL || size <= 0){
		return;//nothing to print
	}
	
	printf("[");
	
	for(int i = 0; i < size-1; i++){
		printf("%i, ", array[i]);
	}
	
	printf("%i]\n", array[size-1]);//print last element with ending bracket
	
}

void sortArray(int *array, const int size){
	int smallest;
	
	for(int i = 0; i < size-1; i++){//Selection Sort
		
		smallest = i;
		for(int j = i+1; j < size; j++){
			
			if(array[smallest] > array[j]){
				smallest = j;
			}
			
		}
		
		if(smallest != i){
			swapElements(array+i, array+smallest);
		}
		
	}
	
}

void swapElements(int *x, int *y){
	int temp = *x;
	*x = *y;
	*y = temp;
}



double findMean(int *array, const int size){
	double sum = 0;//using double so we get double math later
	
	for(int i = 0; i < size; i++){
		sum += array[i];
	}
	
	return sum/size;
}

double findMedian(int *array, const int size){
	double median;
	int* sortedArr = allocIntBlock(size);
	
	for(int i = 0; i < size; i++){
		*(sortedArr+i) = *(array+i);
	}
	
	sortArray(sortedArr, size);
	
	if(size % 2 == 0){ //even
		median = (double)(sortedArr[size/2] + sortedArr[size/2-1]) / 2;
	}else{ //odd
		median = sortedArr[size/2];
	}
	
	free(sortedArr);
	return median;
}


double findStandardDeviation(int *array, const int size, double average){
	double devSum = 0;
	double* devArr = malloc(sizeof(double)*size);
	
	for(int i = 0; i < size; i++){
		*(devArr+i) = (*(array+i) - average) * (*(array+i) - average);
		devSum += *(devArr+i);
	}
	
	free(devArr);
	return sqrt(devSum/(size-1));
}

