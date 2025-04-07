document.addEventListener('DOMContentLoaded', () => {
    //När sidan är laddad ska du hantera de här delarna
    //detta kan ses som vårt program där vi vill köra saker
    initModals()

    //initOpenModals()
    initCloseButtons()
})





function initModals() {
    const buttons = document.querySelectorAll('[data-type="modal"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)
            console.log('d')
            targetElement.classList.add('show')

        })
    })
}




function closeModal(modal) {
    if (modal) {
        modal.classList.remove('flex')

        //Exempel med formulär
        modal.querySelectorAll('form').forEach(form => {
            form.reset()
        })
    }
}






