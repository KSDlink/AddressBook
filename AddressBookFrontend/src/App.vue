<template>
  <div class='container'>
    <div class='row'>
      <div class='col-12'>
        <div class='input-group m-3'>
          <custom-input class='form-control w-auto' v-focus placeholder='Поиск...' v-model='searchQuery'></custom-input>
          <span class='input-group-text'>&#128269;</span>
        </div>
      </div>
      <div>
        <h1>Список контактов</h1>
        <div class='d-flex justify-content-between'>
          <custom-button class='btn btn-success' @click='dialogCreateContact'>Добавить контакт</custom-button>
          <custom-select class='form-select w-auto' v-model='selectedSort' :options='sortOptions'></custom-select>
        </div>
        <custom-dialog :show='dialogVisible' @update:show='hideDialog'>
          <address-form :title='titleDialog' :button='buttonDialog' :contact='dialogContactItem' @clickButton = 'clickDialogHandler'/>
        </custom-dialog>
        <address-list :contacts='sortedAndSearchContacts' @removeContact='removeContact' @updateContact='dialogUpdateContact' v-if='!isContactsLoading'  v-show='!dialogVisible'/>
        <div v-else>Идет загрузка...</div>
      </div>
    </div>
  </div>
  <!-- <div class='app'>
    <div class='app__btns'>
      <custom-button @click='dialogCreateContact'>Добавить контакт</custom-button>
      <custom-select v-model='selectedSort' :options='sortOptions'></custom-select>
    </div> 
    <custom-dialog :show='dialogVisible' @update:show='hideDialog'>
      <address-form :title='titleDialog' :button='buttonDialog' :contact='dialogContactItem' @clickButton = 'clickDialogHandler'/>
    </custom-dialog>
    <address-list :contacts='sortedAndSearchContacts' @removeContact='removeContact' @updateContact='dialogUpdateContact' v-if='!isContactsLoading'/>
    <div v-else>Идет загрузка...</div>
  </div> -->
</template>

<script lang='ts'>
import { defineComponent } from 'vue';
import AddressForm from '@/components/AddressForm.vue'
import AddressList from '@/components/AddressList.vue'
import { contactItem } from '@/types';

export default defineComponent({
  name: 'App',
  components: {
    AddressForm,
    AddressList,
  },
  data () {
    return {
      contacts: [] as contactItem[],
      dialogContactItem: {} as contactItem,
      dialogVisible: false,
      titleDialog: '',
      buttonDialog: '',
      isCreateDialogMode: false,
      isContactsLoading: false,
      selectedSort: '',
      sortOptions: [
        {value: 'firstName', name: 'По имени'},
        {value: 'lastName', name: 'По фамилии'},
      ],
      searchQuery: '',
    }
  },
  methods: {
    hideDialog(){
      this.dialogVisible = false;
    },
    showDialog(){
      this.dialogVisible = true;
    },
    clickDialogHandler(contact: contactItem){
      if(this.isCreateDialogMode){
        this.createContact(contact);
      } else {
        this.updateContact(contact);
      }
    },
    async createContact (contact: contactItem) {
      try {
        const response: contactItem = await fetch('https://localhost:5001/api/contacts/', {method: 'POST', headers:{'Content-Type': 'application/json'}, body: JSON.stringify(contact)})
        .then(response => response.json());
        this.contacts.push(response);
      } catch(e){
        alert('Ошибка добавления контакта: ' + e)
      } finally{
        this.hideDialog();
      }
    },
    async removeContact(contact: contactItem){
      this.contacts = this.contacts.filter(c => c.id !== contact.id); //СПРОСИТЬ
      try {
        await fetch('https://localhost:5001/api/contacts/' + contact.id, {method: 'DELETE'})
      } catch(e){
        alert('Ошибка удаления контакта: ' + e);
      }
    },
    dialogUpdateContact(contact: contactItem) {
      this.titleDialog = 'Изменить контакт';
      this.buttonDialog = 'Изменить';
      this.dialogContactItem = contact;
      this.isCreateDialogMode = false;
      this.showDialog();
    },
    async updateContact(contact: contactItem){ //СПРОСИ
      try {
        console.log(JSON.stringify(contact));
        const response: contactItem = await fetch('https://localhost:5001/api/contacts/', {method: 'PUT', headers:{'Content-Type': 'application/json'}, body: JSON.stringify(contact)})
        .then(response => response.json());
        this.contacts = this.contacts.filter(c => c.id !== contact.id);
        this.contacts.push(response);
      } catch(e){
        alert('Ошибка добавления контакта: ' + e)
      } finally{
        this.hideDialog();
      }
    },
    dialogCreateContact(){
      this.titleDialog = 'Добавить контакт';
      this.buttonDialog = 'Добавить';
      this.dialogContactItem = {} as contactItem;
      this.isCreateDialogMode = true;
      this.showDialog();
    },
    async fetchContacts(){
      try {
        this.isContactsLoading = true;
        const response: contactItem[] = await fetch('https://localhost:5001/api/contacts')
        .then(response => response.json());
        response.forEach( (item: contactItem) => {
            this.contacts.push(item);
        })
      } catch(e){
        alert('Ошибка получения данных с сервера: ' + e);
      } finally {
        this.isContactsLoading = false;
      }
    },
  },
  mounted() {
    this.fetchContacts();
  },
  computed: {
     sortedContacts() :contactItem[]{
       return [...this.contacts].sort((contact1, contact2) => {
         return contact1[this.selectedSort]?.localeCompare(contact2[this.selectedSort]);
        })
      },
    sortedAndSearchContacts(): contactItem[] {
        return this.sortedContacts.filter(contact => contact.firstName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        contact.lastName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        contact.email.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        contact.phoneNumber.toLowerCase().includes(this.searchQuery.toLowerCase())
        )
      }
  },
  watch: {},
});
</script>

<style>
/* * {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
.app {
  padding: 20px;
}
.app__btns {
  margin: 15px 0;
  display: flex;
  justify-content: space-between;
} */
</style>
