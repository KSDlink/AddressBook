import { defineStore } from 'pinia'
import { contactItem } from '@/types'

export const useContacts = defineStore({
  id: 'contacts',
  state: () => ({
    contacts: [] as contactItem[],
    searchQuery: '',
    selectedSort: '',
    sortOptions: [
      {value: 'firstName', name: 'По имени'},
      {value: 'lastName', name: 'По фамилии'},
    ],
    isContactsLoading: false,
  }),
  getters: {
    getContacts(): contactItem[]{
      return [...this.contacts].sort((contact1, contact2) => {
        return contact1[this.selectedSort]?.localeCompare(contact2[this.selectedSort]);
      })
      .filter(contact => contact.firstName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        contact.lastName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        contact.email.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        contact.phoneNumber.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    },
    getIsContactsLoading(): Boolean {
      return this.isContactsLoading;
    }
  },
  actions: {
    async fetchContacts() {
        //Вернуть надпись загрузки
        try {
            const response: contactItem[] = await fetch('https://localhost:5001/api/contacts')
            .then(response => response.json());
            response.forEach( (item: contactItem) => {
                this.contacts.push(item);
            })
        } catch(e){
            alert('Ошибка получения данных с сервера: ' + e);
        }
    },
    async createContact (contact: contactItem) {
      try {
        const response: contactItem = await fetch('https://localhost:5001/api/contacts/', {method: 'POST', headers:{'Content-Type': 'application/json'}, body: JSON.stringify(contact)})
        .then(response => response.json());
        this.contacts.push(response);
      } catch(e){
        alert('Ошибка добавления контакта: ' + e)
      }
    },
    async updateContact(contact: contactItem){
      try {
        console.log(JSON.stringify(contact));
        const response: contactItem = await fetch('https://localhost:5001/api/contacts/', {method: 'PUT', headers:{'Content-Type': 'application/json'}, body: JSON.stringify(contact)})
        .then(response => response.json());
        this.contacts = this.contacts.filter(c => c.id !== contact.id);
        this.contacts.push(response);
      } catch(e){
        alert('Ошибка добавления контакта: ' + e)
      }
    },
    async removeContact(contact: contactItem){
      this.contacts = this.contacts.filter(c => c.id !== contact.id);
      try {
        await fetch('https://localhost:5001/api/contacts/' + contact.id, {method: 'DELETE'})
      } catch(e){
        alert('Ошибка удаления контакта: ' + e);
      }
    },
    // setSearchQuery(query: string) {
    //   this.searchQuery = query;
    // },
    // setSelectedSort(select: string) {
    //   this.selectedSort = select;
    // },
  }
})
