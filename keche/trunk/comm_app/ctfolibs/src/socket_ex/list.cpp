#include "list.h"

 void INIT_LIST_HEAD(struct list_head *list)
{
	list->next = list;
	list->prev = list;
}

/*
 * Insert a new entry between two known consecutive entries.
 *
 * This is only for internal list manipulation where we know
 * the prev/next entries already!
 */
// new_item, head->prev, head
 void __list_add(struct list_head *new_item,
			      struct list_head *prev,
			      struct list_head *next)
{
	next->prev = new_item;
	new_item->next = next;
	new_item->prev = prev;
	prev->next = new_item;
}


/**
 * list_add - add a new entry
 * @new: new entry to be added
 * @head: list head to add it after
 *
 * Insert a new entry after the specified head.
 * This is good for implementing stacks.
 */
void list_add(struct list_head *new_item, struct list_head *head)
{
	__list_add(new_item, head, head->next);
}


/**
 * list_add_tail - add a new entry
 * @new: new entry to be added
 * @head: list head to add it before
 *
 * Insert a new entry before the specified head.
 * This is useful for implementing queues.
 */
void list_add_tail(struct list_head *new_item, struct list_head *head)
{
	__list_add(new_item, head->prev, head);
}

/*
 * Delete a list entry by making the prev/next entries
 * point to each other.
 *
 * This is only for internal list manipulation where we know
 * the prev/next entries already!
 */
 void __list_del(struct list_head * prev, struct list_head * next)
{
	next->prev = prev;
	prev->next = next;
}

/**
 * list_del - deletes entry from list.
 * @entry: the element to delete from the list.
 * Note: list_empty() on entry does not return true after this, the entry is
 * in an undefined state.
 */

void list_del(struct list_head *entry)
{
	__list_del(entry->prev, entry->next);
	entry->next = 0;
	entry->prev = 0;
}

/**
 * 两链表合并处理
 */
void list_merge( struct list_head *head, struct list_head *data )
{
	head->prev->next = data->next ;
	data->prev->next = head ;
	data->next->prev = head->prev ;
	head->prev       = data->prev ;
}

