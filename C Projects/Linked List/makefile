OBJS= main.o LinkedListString.o
CC=gcc
CFLAGS=-c -Wall
output: $(OBJS)
	$(CC) $(OBJS) -o output
main.o: main.c LinkedListString.h
	$(CC) $(CFLAGS) main.c
LinkedListString.o: LinkedListString.c LinkedListString.h
	$(CC) $(CFLAGS) LinkedListString.c
clean:
	rm *.o output
